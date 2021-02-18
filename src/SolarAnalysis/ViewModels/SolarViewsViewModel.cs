﻿// (C) Copyright 2018-2020 by Andrew Nicholas
//
// This file is part of SCaddins.
//
// SCaddins is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// SCaddins is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with SCaddins.  If not, see <http://www.gnu.org/licenses/>.

namespace SCaddins.SolarAnalysis.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Caliburn.Micro;

    internal class SolarViewsViewModel : Screen
    {
        private DateTime creationDate;
        private DateTime endTime;
        private TimeSpan interval;
        private SolarAnalysisManager model;
        private CloseMode selectedCloseMode;
        private DateTime startTime;
        ////private Rectangle windowBounds;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Microsoft.Usage", "CA2213: Disposable fields should be disposed", Justification = "Parameter intialized by Revit", MessageId = "uidoc")]
        private UIDocument uidoc;

        public SolarViewsViewModel(UIDocument uidoc)
        {
            this.uidoc = uidoc;
            selectedCloseMode = CloseMode.Close;
            MassSelection = null;
            FaceSelection = null;
            AnalysisGridSize = 1000;
            Size = SCaddinsApp.WindowManager.Size;
            Left = SCaddinsApp.WindowManager.Left;
            Top = SCaddinsApp.WindowManager.Top;
            model = new SolarAnalysisManager(uidoc);
            creationDate = new DateTime(2018, 06, 21);
            startTime = new DateTime(2018, 06, 21, 9, 0, 0, DateTimeKind.Local);
            endTime = new DateTime(2018, 06, 21, 15, 0, 0, DateTimeKind.Local);
            interval = new TimeSpan(1, 00, 00);
            RotateCurrentView = CanRotateCurrentView;
            if (!CanRotateCurrentView) {
                Create3dViews = true;
            }
        }

        public enum CloseMode
        {
            Close,
            MassSelection,
            FaceSelection,
            Analize,
            Clear
        }

        public static dynamic DefaultViewSettings
        {
            get
            {
                dynamic settings = new ExpandoObject();
                settings.Height = 480;
                settings.Width = 300;
                settings.Icon = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("pack://application:,,,/SCaddins;component/Assets/scaos.png"));
                settings.Title = "Solar Analysis - By Andrew Nicholas";
                settings.ShowInTaskbar = false;
                settings.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
                return settings;
            }
        }

        public static BindableCollection<TimeSpan> Intervals
        {
            get
            {
                var times = new BindableCollection<TimeSpan>();
                times.Add(new TimeSpan(00, 15, 00));
                times.Add(new TimeSpan(00, 30, 00));
                times.Add(new TimeSpan(1, 00, 00));
                return times;
            }
        }

        public double AnalysisGridSize
        {
            get; set;
        }

        public bool CanCreateAnalysisView
        {
            get
            {
                return model.CanCreateAnalysisView;
            }
        }

        public bool CanRotateCurrentView
        {
            get
            {
                return model.CanRotateActiveView;
            }
        }

        public bool Create3dViews
        {
            get
            {
                return model.Create3dViews;
            }

            set
            {
                if (model.Create3dViews != value) {
                    model.Create3dViews = value;
                    NotifyOfPropertyChange(() => CurrentModeSummary);
                    NotifyOfPropertyChange(() => CreateAnalysisView);
                    NotifyOfPropertyChange(() => ShowDateSelectionPanel);
                }
            }
        }

        public bool CreateAnalysisView
        {
            get
            {
                return model.CreateAnalysisView;
            }

            set
            {
                if (model.CreateAnalysisView != value) {
                    model.CreateAnalysisView = value;
                    NotifyOfPropertyChange(() => CurrentModeSummary);
                    NotifyOfPropertyChange(() => CreateAnalysisView);
                    NotifyOfPropertyChange(() => ShowDateSelectionPanel);
                }
            }
        }

        public bool CreateShadowPlans
        {
            get
            {
                return model.CreateShadowPlans;
            }

            set
            {
                if (model.CreateShadowPlans != value) {
                    model.CreateShadowPlans = value;
                    NotifyOfPropertyChange(() => CurrentModeSummary);
                    NotifyOfPropertyChange(() => CreateAnalysisView);
                    NotifyOfPropertyChange(() => ShowDateSelectionPanel);
                }
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return creationDate;
            }

            set
            {
                if (value != creationDate) {
                    var oldStartIndex = StartTimes.IndexOf(SelectedStartTime);
                    var oldEndIndex = EndTimes.IndexOf(SelectedEndTime);
                    creationDate = value;
                    NotifyOfPropertyChange(() => StartTimes);
                    NotifyOfPropertyChange(() => EndTimes);
                    SelectedStartTime = StartTimes[oldStartIndex];
                    SelectedEndTime = EndTimes[oldEndIndex];
                }
            }
        }

        public string CurrentModeSummary
        {
            get
            {
                if (RotateCurrentView) {
                    return "Rotate Current View";
                }
                if (Create3dViews) {
                    return "Create View[s]";
                }
                if (CreateShadowPlans) {
                    return "Create Plans";
                }
                if (CreateAnalysisView) {
                    return "Create Analysis View";
                }
                return "OK";
            }
        }

        public bool EnableRotateCurrentView
        {
            get
            {
                return CanRotateCurrentView;
            }
        }

        public BindableCollection<DateTime> EndTimes
        {
            get
            {
                var times = new BindableCollection<DateTime>();
                for (int hour = 9; hour < 18; hour++) {
                    times.Add(new DateTime(creationDate.Year, creationDate.Month, creationDate.Day, hour, 0, 0, DateTimeKind.Local));
                }
                return times;
            }
        }

        public IList<Reference> FaceSelection
        {
            get; set;
        }

        public double Left
        {
            get; set;
        }

        public IList<Reference> MassSelection
        {
            get; set;
        }

        public bool RotateCurrentView
        {
            get
            {
                return model.RotateCurrentView;
            }

            set
            {
                if (model.RotateCurrentView != value) {
                    model.RotateCurrentView = value;
                    NotifyOfPropertyChange(() => CurrentModeSummary);
                }
            }
        }

        public CloseMode SelectedCloseMode
        {
            get
            {
                return selectedCloseMode;
            }

            set
            {
                selectedCloseMode = value;
            }
        }

        public DateTime SelectedEndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                if (value != endTime) {
                    endTime = value;
                    NotifyOfPropertyChange(() => SelectedEndTime);
                }
            }
        }

        public string SelectedFaceInformation
        {
            get
            {
                int f = FaceSelection != null ? FaceSelection.Count : 0;
                return $"Selected Faces: {f}";
            }
        }

        public TimeSpan SelectedInterval
        {
            get => interval;
            set => interval = value;
        }

        public string SelectedMassInformation
        {
            get
            {
                int m = MassSelection != null ? MassSelection.Count : 0;
                return $"Selected Masses: {m}";
            }
        }

        public DateTime SelectedStartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                if (value != startTime) {
                    startTime = value;
                    NotifyOfPropertyChange(() => SelectedStartTime);
                }
            }
        }

        public bool ShowDateSelectionPanel
        {
            get
            {
                return CreateShadowPlans || Create3dViews;
            }
        }

        public System.Windows.Size Size
        {
            get; set;
        }

        public BindableCollection<DateTime> StartTimes
        {
            get
            {
                var times = new BindableCollection<DateTime>();
                for (int hour = 8; hour < 17; hour++) {
                    times.Add(new DateTime(creationDate.Year, creationDate.Month, creationDate.Day, hour, 0, 0, DateTimeKind.Local));
                }
                return times;
            }
        }

        /// <summary>
        /// Top left coord of main dialog
        /// </summary>
        public double Top
        {
            get; set;
        }

        public string ViewInformation
        {
            get
            {
                return model.ActiveIewInformation;
            }
        }

        /// <summary>
        /// Attempt to re-open the main dialog after a user selection has been made.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="resize">Resize and relocate to previous location.</param>
        public static void Respawn(SolarViewsViewModel viewModel, bool resize)
        {
            var settings = DefaultViewSettings;

            // Reopen window with previous position / size
            if (resize) {
                settings.Width = viewModel.Size.Width;
                settings.Height = viewModel.Size.Height;
                settings.Top = viewModel.Top;
                settings.Left = viewModel.Left;
                settings.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                settings.SizeToContent = System.Windows.SizeToContent.Manual;
            }
            viewModel.SelectedCloseMode = CloseMode.Close;
            SCaddinsApp.WindowManager.ShowDialog(viewModel, null, settings);
        }

        public void Clear()
        {
            selectedCloseMode = CloseMode.Clear;
            TryClose(true);
        }

        /// <summary>
        /// Run the selected mode.
        /// </summary>
        public void OK()
        {
            if (model.CreateAnalysisView) {
                TryClose(true);
            } else {
                var log = new ModelSetupWizard.TransactionLog(CurrentModeSummary);
                model.StartTime = SelectedStartTime.ToLocalTime();
                model.EndTime = SelectedEndTime.ToLocalTime();
                model.ExportTimeInterval = SelectedInterval;
                model.Go(log);
                SCaddinsApp.WindowManager.ShowMessageBox(log.Summary());
                DockablePaneId docablePaneId = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
                DockablePane dP = new DockablePane(docablePaneId);
                dP.Show();
            }
        }

        public void RunAnalysis()
        {
            selectedCloseMode = CloseMode.Analize;
            TryClose(true);
        }

        public void SelectAnalysisFaces()
        {
            selectedCloseMode = CloseMode.FaceSelection;
            TryClose(true);
        }

        public void SelectMasses()
        {
            selectedCloseMode = CloseMode.MassSelection;
            TryClose(true);
        }

        protected override void OnDeactivate(bool close)
        {
            // Get old size/location for respawning (if required)
            Size = SCaddinsApp.WindowManager.Size;
            Left = SCaddinsApp.WindowManager.Left;
            Top = SCaddinsApp.WindowManager.Top;

            base.OnDeactivate(true);

            switch (selectedCloseMode) {
                case CloseMode.FaceSelection:
                try {
                    FaceSelection = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Face, "Select Faces");
                } catch (Autodesk.Revit.Exceptions.OperationCanceledException ex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(ex.Message);
                    FaceSelection = null;
                } catch (Autodesk.Revit.Exceptions.ArgumentNullException anex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(anex.Message);
                    FaceSelection = null;
                } catch (Autodesk.Revit.Exceptions.ArgumentOutOfRangeException aoorex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(aoorex.Message);
                    FaceSelection = null;
                } catch (Autodesk.Revit.Exceptions.ForbiddenForDynamicUpdateException ffduex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(ffduex.Message);
                    FaceSelection = null;
                }
                Respawn(this, true);
                break;

                case CloseMode.MassSelection:
                try {
                    MassSelection = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select Masses");
                } catch (Autodesk.Revit.Exceptions.OperationCanceledException ex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(ex.Message);
                    MassSelection = null;
                } catch (Autodesk.Revit.Exceptions.ArgumentNullException anex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(anex.Message);
                    MassSelection = null;
                } catch (Autodesk.Revit.Exceptions.ArgumentOutOfRangeException aoorex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(aoorex.Message);
                    MassSelection = null;
                } catch (Autodesk.Revit.Exceptions.ForbiddenForDynamicUpdateException ffduex) {
                    SCaddinsApp.WindowManager.ShowMessageBox(ffduex.Message);
                    MassSelection = null;
                }
                Respawn(this, true);
                break;
            }
        }
    }
}