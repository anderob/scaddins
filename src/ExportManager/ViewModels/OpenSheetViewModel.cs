﻿// (C) Copyright 2018 by Andrew Nicholas
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

using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace SCaddins.ExportManager.ViewModels 
{
    class OpenSheetViewModel : Screen
    {
        private OpenableView selectedSearchResult;
        private ObservableCollection<OpenableView> viewsInDoc;
        private CollectionViewSource searchResults;
        private string searchInput;
        private bool ctrlDown;

        public string SearchInput
        {
            get
            {
                return searchInput;
            }
            set
            {
                if (value != searchInput) {
                    searchInput = value;
                    int successCount = 0;
                    int maxCount = 10 + (searchInput.Length - 1) * 5;
                    SearchResults.Filter = v =>
                    {
                        OpenableView ov = v as OpenableView;
                        if (successCount < maxCount && ov.IsMatch(searchInput)) {
                            successCount++;
                            return ov.IsMatch(searchInput);
                        }
                        return false;
                    };
                    NotifyOfPropertyChange(() => ShowHelpText);
                    NotifyOfPropertyChange(() => ShowExtendedHelpText);
                    NotifyOfPropertyChange(() => ShowSearchresults);
                    NotifyOfPropertyChange(() => ShowSearchresults);
                }
            }
        }

        public bool ShowHelpText
        {
            get
            {
                return searchInput.Length < 1;
            }
        }

        public bool ShowExtendedHelpText
        {
            get
            {
                return searchInput == "?";
            }
        }

        public bool ShowSearchresults
        {
            get
            {
                return !ShowExtendedHelpText;
            }
        }

        public OpenableView SelectedSearchResult
        {
            get
            {
                return selectedSearchResult;
            }
            set
            {
                if (value != selectedSearchResult) {
                    selectedSearchResult = value;
                }
            }
        }

        public ICollectionView SearchResults
        {
            get { return this.searchResults.View; }
        }

        public void SelectNext()
        {
            SearchResults.MoveCurrentToNext();
            if (SearchResults.IsCurrentAfterLast) SearchResults.MoveCurrentToFirst();
        }

        public void SelectPrevious()
        {
            SearchResults.MoveCurrentToPrevious();
            if (SearchResults.IsCurrentBeforeFirst) SearchResults.MoveCurrentToLast();
        }

        public void MouseDoubleClick()
        {
            selectedSearchResult.Open();
            TryClose();
        }

        public void KeyDown(System.Windows.Input.KeyEventArgs args)
        {
            if (args.Key == System.Windows.Input.Key.Escape) {
                TryClose();
            }
            if (args.Key == System.Windows.Input.Key.Enter) {
                selectedSearchResult.Open();
                TryClose();
            }
            if (ctrlDown && args.Key == System.Windows.Input.Key.J)
            {
                SelectNext();
            }
            if (ctrlDown && args.Key == System.Windows.Input.Key.K)
            {
                SelectPrevious();
            }
            if (args.Key == System.Windows.Input.Key.LeftCtrl)
            {
                ctrlDown = true;
            }
        }

        public void KeyUp(System.Windows.Input.KeyEventArgs args)
        {
            if (args.Key == System.Windows.Input.Key.LeftCtrl)
            {
                ctrlDown = false;
            }
        }

        public void Exit()
        {
            TryClose();
        }

        public OpenSheetViewModel(Autodesk.Revit.DB.Document doc)
        {
            this.searchResults = new CollectionViewSource();
            this.searchResults.Source = OpenSheet.ViewsInModel(doc);
            selectedSearchResult = null;
            ctrlDown = false;
            SearchInput = string.Empty;
        }
    }
}