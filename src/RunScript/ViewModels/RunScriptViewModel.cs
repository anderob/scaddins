﻿// (C) Copyright 2019 by Andrew Nicholas
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

namespace SCaddins.RunScript.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Xml;
    using Caliburn.Micro;
    using ICSharpCode.AvalonEdit.Highlighting;

    internal class RunScriptViewModel : Screen
    {
        private string output;
        private string script;
        private Caliburn.Micro.BindableCollection<String> outputList;
        private string selectedOutputList;
        private int caretColumnPosition;

        public RunScriptViewModel()
        {
            caretColumnPosition = 5;
            LightMode();
            output = string.Empty;
            outputList = new BindableCollection<string>();
            Script =
@"using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using SCaddins;

public static void Main(Document doc)
{
    using (var t = new Transaction(doc)) {
        t.Start(""Run Script"");
        var fec = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rooms);
            foreach (var r in fec)
            {
                var room = r as Autodesk.Revit.DB.Architecture.Room;
                room.Name = room.Name.ToUpper();
            }
            t.Commit();
        }
}
";
        }

        public static dynamic DefaultViewSettings
        {
            get
            {
                dynamic settings = new ExpandoObject();
                settings.Height = 480;
                settings.Width = 300;
                settings.Title = "Run (cs)Script";
                settings.ShowInTaskbar = false;
                settings.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
                return settings;
            }
        }

        public string Script
        {
            get
            {
                return script;
            }
            set
            {
                script = value;
                NotifyOfPropertyChange(() => Script);
            }
        }

        public int CaretColumnPosition
        {
            get
            {
                return caretColumnPosition;
            }
            set
            {
                caretColumnPosition = value;
                NotifyOfPropertyChange(() => CaretColumnPosition);
            }
        }

        public BindableCollection<String> OutputList
        {
            get
            {
                outputList.Clear();
                if (!string.IsNullOrEmpty(output)) {
                    using (StringReader sr = new StringReader(Output)) {
                        string line;
                        while ((line = sr.ReadLine()) != null) {
                            outputList.Add(line.Substring(line.IndexOf("(")));
                        }
                    }
                }
                return outputList;
            }
        }

        public string SelectedOutputList
        {
            get
            {
                return selectedOutputList;
            }
            set
            {
                selectedOutputList = value;
//                SCaddinsApp.WindowManager.ShowMessageBox(selectedOutputList);
//                var a = selectedOutputList.IndexOf(@"(");
//                var b = selectedOutputList.IndexOf(@",");
//                var s = selectedOutputList.Substring(a + 1, b - a - 1);
//                SCaddinsApp.WindowManager.ShowMessageBox(s);
//                int i = 0;
//                if (int.TryParse(s, out i)) CaretColumnPosition = i;
            }
        }

        public string Output
        {
            get
            {
                return output;
            }

            set
            {
                output = value;
                NotifyOfPropertyChange(() => Output);
                NotifyOfPropertyChange(() => OutputList);
            }
        }

        public IHighlightingDefinition SyntaxHighlightingTheme
        {
            get; set;
        }

        public System.Windows.Media.Brush Background
        {
            get;set;
        }

        public void DarkMode()
        {
            LoadTheme("CSharp-Mode-Molokai.xshd");
            Background = System.Windows.Media.Brushes.Black;
            NotifyOfPropertyChange(() => SyntaxHighlightingTheme);
            NotifyOfPropertyChange(() => Background);
        }

        public void LightMode()
        {
            LoadTheme("CSharp-Mode.xshd");
            Background = System.Windows.Media.Brushes.White;
            NotifyOfPropertyChange(() => SyntaxHighlightingTheme);
            NotifyOfPropertyChange(() => Background);
        }

        public void LoadScriptFromFile()
        {
            var s = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var p = System.IO.Path.Combine(s, "SCaddins", "Script.cs");
            if (!File.Exists(p)) {
                return;
            }
            Script = System.IO.File.ReadAllText(p);
        }

        public void LoadTheme(string themeFile)
        {
            string dir = @"C:\Code\cs\scaddins\src\RunScript\Resources\";
            if (File.Exists(dir + themeFile)) {
                try {
                    Stream xshd_stream = File.OpenRead(dir + themeFile);
                    XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
                    var theme = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(xshd_reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                    SyntaxHighlightingTheme = theme;
                    xshd_reader.Close();
                    xshd_stream.Close();
                }
                catch (Exception e) {
                    SCaddinsApp.WindowManager.ShowMessageBox(e.Message);
                }
            }
        }

        public void Run()
        {
            var compileResults = string.Empty;
            var result = RunScriptCommand.VerifyScript(RunScriptCommand.ClassifyScript(Script), out compileResults);
            Output = compileResults;
            if (result) {
                TryClose(true);
            }
        }

        public void SaveAs()
        {
            Save();
        }

        public void Save()
        {
            var s = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var p = System.IO.Path.Combine(s, "SCaddins");
            if (!Directory.Exists(p)) {
                Directory.CreateDirectory(p);
            }
            System.IO.File.WriteAllText(System.IO.Path.Combine(p, "Script.cs"), Script);
        }
    }
}