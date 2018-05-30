﻿using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace SCaddins.RoomConvertor.ViewModels
{

    public class RoomConvertorViewModel : Screen
    {
        private RoomConversionManager manager;
        private List<RoomConversionCandidate> rooms;
        private bool massCreationMode;
        private bool sheetCreationMode;
        public RoomConversionCandidate selectedRoom;
        private RoomFilter filter;
        List<RoomConversionCandidate> selectedRooms = new List<RoomConversionCandidate>();

        public RoomConvertorViewModel(RoomConversionManager manager)
        {
            this.manager = manager;
            this.rooms = new List<RoomConversionCandidate>(manager.Candidates);
            MassCreationMode = true;
            SheetCreationMode = false;
            filter = new RoomFilter();
        }

        public bool SheetCreationMode
        {
            get
            {
                return sheetCreationMode;
            }
            set
            {
                if (value != sheetCreationMode)
                {
                    sheetCreationMode = value;
                    NotifyOfPropertyChange(() => SheetCreationMode);
                    NotifyOfPropertyChange(() => RunButtonText);
                }
            }
        }

        public bool MassCreationMode
        {
            get
            {
                return massCreationMode;
            }
            set
            {
                if (value != massCreationMode)
                {
                    massCreationMode = value;
                    NotifyOfPropertyChange(() => MassCreationMode);
                    NotifyOfPropertyChange(() => RunButtonText);
                }
            }
        }

        public bool RoomInformationIsAvailable
        {
            get
            {
                return SelectedRoom != null;
            }
        }

        public ObservableCollection<RoomConversionCandidate> Rooms
        {
            get { return new ObservableCollection<RoomConversionCandidate>(rooms.Where(r => filter.PassesFilter(r.Room))); }
        }

        public void RowSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs obj)
        {
            selectedRooms.AddRange(obj.AddedItems.Cast<RoomConversionCandidate>());
            obj.RemovedItems.Cast<RoomConversionCandidate>().ToList().ForEach(w => selectedRooms.Remove(w));
            NotifyOfPropertyChange(() => SelectionInformation);
        }

        public RoomConversionCandidate SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
            set
            {
                if (value != selectedRoom)
                {
                    selectedRoom = value;
                    NotifyOfPropertyChange(() => RoomParameters);
                    NotifyOfPropertyChange(() => RoomInformationIsAvailable);
                    NotifyOfPropertyChange(() => SelectionInformation);
                }
            }
        }

        public List<RoomParameter> RoomParameters
        {
            get
            {
                return SelectedRoom.RoomParameters; 
            }
        }

        public string SelectionInformation
        {
            get
            {
                return Rooms.Count + " Rooms, " + selectedRooms.Count + " Selected";
            }
        }

        public void AddFilter()
        {
            dynamic settings = new System.Dynamic.ExpandoObject();
            settings.Height = 480;
            settings.Width = 768;
            settings.Title = "Filter Rooms";
            settings.ShowInTaskbar = false;
            settings.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            var bs = SCaddinsApp.Bootstrapper;
            var windowManager = SCaddinsApp.WindowManager;
            var vm = new ViewModels.RoomFilterViewModel(manager, filter);
            windowManager.ShowDialog(vm, null, settings);
            NotifyOfPropertyChange(() => Rooms);
            NotifyOfPropertyChange(() => SelectionInformation);
        }

        public void RemoveFilter()
        {
            filter.Clear();
            NotifyOfPropertyChange(() => Rooms);
            NotifyOfPropertyChange(() => SelectionInformation);
        }

        public void run()
        {
            if (MassCreationMode) {
                manager.CreateRoomMasses(selectedRooms);
            } else {
                //Sheet creation mode.
                //Get some parameters first
                dynamic settings = new System.Dynamic.ExpandoObject();
                settings.Height = 480;
                settings.Width = 480;
                settings.Title = "Sheet Creation Options";
                settings.ShowInTaskbar = false;
                settings.SizeToContent = System.Windows.SizeToContent.Height;
                var bs = SCaddinsApp.Bootstrapper;
                var windowManager = SCaddinsApp.WindowManager;
                var vm = new ViewModels.RoomToSheetWizardViewModel(manager);
                windowManager.ShowDialog(vm, null, settings);

                manager.CreateViewsAndSheets(selectedRooms);
            }
        }

        public string RunButtonText
        {
            get
            {
                if (MassCreationMode) {
                    return "Create Masses";
                } else {
                    return "Create Sheets";
                }
            }
        }

    }
}

