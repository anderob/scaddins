﻿namespace SCaddins.ExportSchedules.ViewModels
{
    using System.Collections.Generic;
    using System.Dynamic;
    using Caliburn.Micro;

    public class OptionsViewModel : Screen
    {
        public static dynamic DefaultWindowSettings
        {
            get
            {
                dynamic settings = new ExpandoObject();
                settings.Height = 320;
                settings.Width = 480;
                settings.Title = "Export Schedules - Options";
                settings.ShowInTaskbar = false;
                settings.SizeToContent = System.Windows.SizeToContent.Manual;
                settings.ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
                return settings;
            }
        }

        public bool ExportTitle
        {
            get
            {
                return Settings.Default.ExportTitle;
            }

            set
            {
                Settings.Default.ExportTitle = value;
            }
        }

        public bool ExportColumnHeaders
        {
            get
            {
                return Settings.Default.ExportColumnHeader;
            }

            set
            {
                Settings.Default.ExportColumnHeader = value;
                if (value == false)
                {
                    IncludeGroupedColumnHeaders = false;
                }
                NotifyOfPropertyChange(() => ExportColumnHeaders);
                NotifyOfPropertyChange(() => IncludeGroupedColumnHeaders);
            }
        }

        public bool IncludeGroupedColumnHeaders
        {
            get
            {
                return Settings.Default.IncludeGroupedColumnHeaders;
            }

            set
            {
                Settings.Default.IncludeGroupedColumnHeaders = value;
            }
        }

        public bool ExportGroupHeaderAndFooters
        {
            get
            {
                return Settings.Default.ExportGrouppHeaderAndFooters;
            }

            set
            {
                Settings.Default.ExportGrouppHeaderAndFooters = value;
            }
        }

        public Dictionary<string, string> FieldDelimiters => Utilities.GetFieldDelimiters();

        public string SelectedFieldDelimiter
        {
            get
            {
                return Settings.Default.FieldDelimiter;
            }

            set
            {
                Settings.Default.FieldDelimiter = value;
            }
        }

        public Dictionary<string, string> TextQualifiers => Utilities.GetFieldTextQualifiers();

        public string SelectedTextQualifier
        {
            get
            {
                return Settings.Default.TextQualifier;
            }

            set
            {
                Settings.Default.TextQualifier = value;
            }
        }

        public void OK()
        {
            Settings.Default.Save();
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
