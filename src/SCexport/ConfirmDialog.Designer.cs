﻿// (C) Copyright 2012-2013 by Andrew Nicholas (andrewnicholas@iinet.net.au)
//
// This file is part of SCexport.
//
// SCexport is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// SCexport is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with SCexport.  If not, see <http://www.gnu.org/licenses/>.

namespace SCaddins.SCexport
{
    public partial class ConfirmationDialog
    {
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox confirmOverwriteCheckBox;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button yesButton;
        
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">
        /// True if managed resources should be disposed; otherwise, false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (this.components != null) {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor.
        /// The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmationDialog));
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.confirmOverwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // yesButton
            // 
            this.yesButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.yesButton.Location = new System.Drawing.Point(392, 95);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 0;
            this.yesButton.Text = "Yes";
            this.yesButton.UseVisualStyleBackColor = true;
            // 
            // noButton
            // 
            this.noButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.noButton.Location = new System.Drawing.Point(311, 95);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(75, 23);
            this.noButton.TabIndex = 1;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.Button2Click);
            // 
            // confirmOverwriteCheckBox
            // 
            this.confirmOverwriteCheckBox.Location = new System.Drawing.Point(12, 95);
            this.confirmOverwriteCheckBox.Name = "confirmOverwriteCheckBox";
            this.confirmOverwriteCheckBox.Size = new System.Drawing.Size(282, 24);
            this.confirmOverwriteCheckBox.TabIndex = 2;
            this.confirmOverwriteCheckBox.Text = "Always ask for confirmation before overwriting?";
            this.confirmOverwriteCheckBox.UseVisualStyleBackColor = true;
            this.confirmOverwriteCheckBox.CheckedChanged += new System.EventHandler(this.ConfirmOverwriteCheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(455, 77);
            this.textBox1.TabIndex = 3;
            // 
            // ConfirmationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 127);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.confirmOverwriteCheckBox);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Confirm file overwrite?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }        
    }
}

/* vim: set ts=4 sw=4 nu expandtab: */
