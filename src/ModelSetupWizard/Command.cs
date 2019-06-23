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
namespace SCaddins.ModelSetupWizard
{
    using System.Dynamic;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class Command : IExternalCommand
    {
        public Result Execute(
                ExternalCommandData commandData,
                ref string message,
                ElementSet elements)
        {
            if (commandData == null) {
                return Result.Failed;
            }

            Document doc = commandData.Application.ActiveUIDocument.Document;

            // Enable worsharing if required
            // Fix me add this at start
            if (doc.IsWorkshared == false)
            {
                var addWorksets = SCaddinsApp.WindowManager.ShowYesNoDialog("Enable Worksharing", "Worksaring is not yet enabled. Do you want to enable it now?", false);
                if (addWorksets)
                {
                    doc.EnableWorksharing("Shared Levels and Grids", "Workset1");
                }
            }

            dynamic settings = new ExpandoObject();
            settings.Height = 480;
            settings.Width = 360;
            settings.Title = "Model Setup Wizard - By A.Nicholas";
            settings.ShowInTaskbar = false;
            settings.SizeToContent = System.Windows.SizeToContent.Width;

            var vm = new ViewModels.ModelSetupWizardViewModel(doc);
            SCaddinsApp.WindowManager.ShowDialog(vm, null, settings);

            return Result.Succeeded;
        }
    }
}