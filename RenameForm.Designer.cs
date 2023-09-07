using DarkUI.Controls;
using DarkUI.Forms;
using MetroSet_UI.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace P5RStringEditor
{
    partial class RenameForm : DarkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameForm));
            metroSetLabel_ProjectName = new MetroSetLabel();
            metroSetTextBox_NewName = new DarkTextBox();
            tableLayoutPanel_Settings = new TableLayoutPanel();
            tableLayoutPanel_ProjectName = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            metroSetButton_Cancel = new DarkButton();
            metroSetButton_Save = new DarkButton();
            tableLayoutPanel_Settings.SuspendLayout();
            tableLayoutPanel_ProjectName.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // metroSetLabel_ProjectName
            // 
            metroSetLabel_ProjectName.Anchor = AnchorStyles.Right;
            metroSetLabel_ProjectName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            metroSetLabel_ProjectName.ForeColor = Color.FromArgb(220, 220, 220);
            metroSetLabel_ProjectName.Location = new Point(2, 45);
            metroSetLabel_ProjectName.Margin = new Padding(2, 0, 2, 0);
            metroSetLabel_ProjectName.Name = "metroSetLabel_ProjectName";
            metroSetLabel_ProjectName.Size = new Size(93, 31);
            metroSetLabel_ProjectName.TabIndex = 25;
            metroSetLabel_ProjectName.Text = "New Name:";
            metroSetLabel_ProjectName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // metroSetTextBox_NewName
            // 
            metroSetTextBox_NewName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroSetTextBox_NewName.BackColor = Color.FromArgb(69, 73, 74);
            metroSetTextBox_NewName.BorderStyle = BorderStyle.FixedSingle;
            metroSetTextBox_NewName.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            metroSetTextBox_NewName.ForeColor = Color.FromArgb(220, 220, 220);
            metroSetTextBox_NewName.Location = new Point(99, 48);
            metroSetTextBox_NewName.Margin = new Padding(2);
            metroSetTextBox_NewName.Name = "metroSetTextBox_NewName";
            metroSetTextBox_NewName.Size = new Size(388, 26);
            metroSetTextBox_NewName.TabIndex = 26;
            // 
            // tableLayoutPanel_Settings
            // 
            tableLayoutPanel_Settings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel_Settings.BackColor = Color.FromArgb(60, 63, 65);
            tableLayoutPanel_Settings.ColumnCount = 1;
            tableLayoutPanel_Settings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel_Settings.Controls.Add(tableLayoutPanel_ProjectName, 0, 0);
            tableLayoutPanel_Settings.Controls.Add(tableLayoutPanel3, 0, 3);
            tableLayoutPanel_Settings.Location = new Point(8, 11);
            tableLayoutPanel_Settings.Margin = new Padding(2);
            tableLayoutPanel_Settings.Name = "tableLayoutPanel_Settings";
            tableLayoutPanel_Settings.RowCount = 4;
            tableLayoutPanel_Settings.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tableLayoutPanel_Settings.RowStyles.Add(new RowStyle(SizeType.Percent, 0F));
            tableLayoutPanel_Settings.RowStyles.Add(new RowStyle(SizeType.Percent, 0F));
            tableLayoutPanel_Settings.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel_Settings.Size = new Size(493, 195);
            tableLayoutPanel_Settings.TabIndex = 28;
            // 
            // tableLayoutPanel_ProjectName
            // 
            tableLayoutPanel_ProjectName.BackColor = Color.FromArgb(60, 63, 65);
            tableLayoutPanel_ProjectName.ColumnCount = 2;
            tableLayoutPanel_ProjectName.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel_ProjectName.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel_ProjectName.Controls.Add(metroSetTextBox_NewName, 1, 0);
            tableLayoutPanel_ProjectName.Controls.Add(metroSetLabel_ProjectName, 0, 0);
            tableLayoutPanel_ProjectName.Dock = DockStyle.Fill;
            tableLayoutPanel_ProjectName.Location = new Point(2, 2);
            tableLayoutPanel_ProjectName.Margin = new Padding(2);
            tableLayoutPanel_ProjectName.Name = "tableLayoutPanel_ProjectName";
            tableLayoutPanel_ProjectName.RowCount = 1;
            tableLayoutPanel_ProjectName.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel_ProjectName.Size = new Size(489, 122);
            tableLayoutPanel_ProjectName.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = Color.FromArgb(60, 63, 65);
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.Controls.Add(metroSetButton_Cancel, 1, 0);
            tableLayoutPanel3.Controls.Add(metroSetButton_Save, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(2, 128);
            tableLayoutPanel3.Margin = new Padding(2);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(489, 65);
            tableLayoutPanel3.TabIndex = 34;
            // 
            // metroSetButton_Cancel
            // 
            metroSetButton_Cancel.DialogResult = DialogResult.Cancel;
            metroSetButton_Cancel.Dock = DockStyle.Fill;
            metroSetButton_Cancel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            metroSetButton_Cancel.Location = new Point(295, 2);
            metroSetButton_Cancel.Margin = new Padding(2);
            metroSetButton_Cancel.Name = "metroSetButton_Cancel";
            metroSetButton_Cancel.Padding = new Padding(3, 4, 3, 4);
            metroSetButton_Cancel.Size = new Size(93, 61);
            metroSetButton_Cancel.TabIndex = 0;
            metroSetButton_Cancel.Text = "Cancel";
            metroSetButton_Cancel.Click += Cancel_Click;
            // 
            // metroSetButton_Save
            // 
            metroSetButton_Save.Dock = DockStyle.Fill;
            metroSetButton_Save.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            metroSetButton_Save.Location = new Point(392, 2);
            metroSetButton_Save.Margin = new Padding(2);
            metroSetButton_Save.Name = "metroSetButton_Save";
            metroSetButton_Save.Padding = new Padding(3, 4, 3, 4);
            metroSetButton_Save.Size = new Size(95, 61);
            metroSetButton_Save.TabIndex = 1;
            metroSetButton_Save.Text = "Save";
            metroSetButton_Save.Click += Save_Click;
            // 
            // RenameForm
            // 
            AcceptButton = metroSetButton_Save;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = metroSetButton_Cancel;
            ClientSize = new Size(509, 224);
            Controls.Add(tableLayoutPanel_Settings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "RenameForm";
            Padding = new Padding(1, 0, 1, 2);
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Rename";
            tableLayoutPanel_Settings.ResumeLayout(false);
            tableLayoutPanel_ProjectName.ResumeLayout(false);
            tableLayoutPanel_ProjectName.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MetroSetLabel metroSetLabel_ProjectName;
        private DarkTextBox metroSetTextBox_NewName;
        private TableLayoutPanel tableLayoutPanel_Settings;
        private TableLayoutPanel tableLayoutPanel_ProjectName;
        private TableLayoutPanel tableLayoutPanel3;
        private DarkButton metroSetButton_Cancel;
        private DarkButton metroSetButton_Save;
    }
}