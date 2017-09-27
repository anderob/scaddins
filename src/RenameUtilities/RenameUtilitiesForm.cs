﻿
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Autodesk.Revit.DB;

namespace SCaddins.RenameUtilities
{
    /// <summary>
    /// Description of Form1.
    /// </summary>
    public partial class Form1 : System.Windows.Forms.Form
    {
        RenameManager manager;
        
        public Form1(RenameManager manager)
        {
            this.manager = manager;
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            PopulateCategoryComboBox();
            PopulatePresetsComboBox();
            //dataGridView1.DataSource = manager.GetRoomParameters();
            //dataGridView1.Refresh;
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        private void PopulateCategoryComboBox()
        {
            comboBox1.Items.Add("Rooms");
            comboBox1.Items.Add("Text");
            comboBox1.Items.Add("Views");
            comboBox1.Items.Add("Sheets");
            comboBox1.Items.Add("Revisions");
            comboBox1.Items.Add("Walls");
            comboBox1.Items.Add("Doors");
            comboBox1.Items.Add("Floors");
            comboBox1.Items.Add("Roofs");
        }
        
        private void PopulatePresetsComboBox()
        {
            comboBoxPresets.Items.Add("Custom");
            comboBoxPresets.Items.Add("Uppercase");
            comboBoxPresets.Items.Add("Lowercase");
            comboBoxPresets.Items.Add("TitleCase");
            comboBoxPresets.Items.Add("Lazy Increment");
            comboBoxPresets.Items.Add("Smart Increment");
            comboBoxPresets.Items.Add("Mirror");
        }
               
        void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Rooms") {
                listBox1.DataSource = manager.GetParametersByCategory(BuiltInCategory.OST_Rooms);
            }
            if (comboBox1.Text == "Views") {
                listBox1.DataSource = manager.GetParametersByCategory(BuiltInCategory.OST_Views);    
            }
            if (comboBox1.Text == "Sheets") {
                listBox1.DataSource = manager.GetParametersByCategory(BuiltInCategory.OST_Sheets);    
            }
            if (comboBox1.Text == "Walls") {
                listBox1.DataSource = manager.GetParametersByCategory(BuiltInCategory.OST_Walls);    
            }
            if (comboBox1.Text == "Floors") {
                listBox1.DataSource = manager.GetParametersByCategory(BuiltInCategory.OST_Floors);    
            }
            if (comboBox1.Text == @"Text") {
                listBox1.DataSource = manager.GetParametersByCategory(BuiltInCategory.OST_TextNotes);    
            }
            listBox1.DisplayMember = "Name";    
        }
        
        void Panel2Paint(object sender, PaintEventArgs e)
        {
          
        }
        
        void Panel1Paint(object sender, PaintEventArgs e)
        {
          
        }
        
        void Button3Click(object sender, EventArgs e)
        {
          
        }
        
        private static string GetReplacementResult(string s, string pattern, string replacement)
        {
            if (string.IsNullOrWhiteSpace(replacement) || string.IsNullOrEmpty(replacement)){
                   return s;
            }
            return Regex.Replace(s, pattern, replacement);
        }
        
        void DataGridToUpper()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                RenameCandidate candidate = (RenameCandidate)row.DataBoundItem;
                candidate.NewValue = candidate.OldValue.ToUpper();
            }
            dataGridView1.Refresh();
        }
        
        void DataGridToLower()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                RenameCandidate candidate = (RenameCandidate)row.DataBoundItem;
                candidate.NewValue = candidate.OldValue.ToLower();
            }
            dataGridView1.Refresh();
        }
        
        void UpdateDataGrid()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                RenameCandidate candidate = (RenameCandidate)row.DataBoundItem;
                candidate.NewValue = GetReplacementResult(candidate.OldValue, textBoxFind.Text, textBoxReplace.Text);
            }
            dataGridView1.Refresh();
        }
        
        void ClearFindAndReplace()
        {
            textBoxFind.Text = string.Empty;
            textBoxReplace.Text = string.Empty;    
        }
        
        void EnableFindAndReplace(bool enable)
        {
            textBoxFind.Enabled = enable;
            textBoxReplace.Enabled = enable;  
            ClearFindAndReplace();
        }
        
        void ComboBoxPresetsSelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBoxPresets.Text == "Custom") {
                EnableFindAndReplace(true);
            }
            if(comboBoxPresets.Text == "Uppercase") {
                EnableFindAndReplace(false);
                DataGridToUpper();
            }
            if(comboBoxPresets.Text == "Lowercase") {
                DataGridToLower();
            }
        }
        
        public void SetRenameCandidates(List<RenameCandidate> candidates)
        {
            dataGridView1.DataSource = candidates;    
        }
                       
        void DataGridView1RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            RenameCandidate candidate = (RenameCandidate)(dataGridView1.Rows[e.RowIndex].DataBoundItem);
            if (candidate.ValueChanged()) {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            } else {
                 dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;    
            }  
        }
        
        void TextBoxReplaceTextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();    
        }
        
        void ListBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            RenameParameter rp = (RenameParameter)(listBox1.SelectedItem);
            SetRenameCandidates(manager.GetParameterValues(rp.Parameter, rp.Category));
        }
        
        void TextBoxFindTextChanged(object sender, EventArgs e)
        {
             UpdateDataGrid();  
        }
        
        void Button1Click(object sender, EventArgs e)
        {
            List<RenameCandidate> renameCandidates = new List<RenameCandidate>();
             foreach (DataGridViewRow row in this.dataGridView1.Rows) {
                RenameCandidate candidate = (RenameCandidate)row.DataBoundItem;
                renameCandidates.Add(candidate);
            }
            manager.Rename(renameCandidates);
        }
        
        void ButtonRenameSelectedClick(object sender, EventArgs e)
        {
             List<RenameCandidate> renameCandidates = new List<RenameCandidate>();
             foreach (DataGridViewRow row in this.dataGridView1.SelectedRows) {
                RenameCandidate candidate = (RenameCandidate)row.DataBoundItem;
                renameCandidates.Add(candidate);
            }
            manager.Rename(renameCandidates);  
        }
    }
}