using MetroSet_UI.Controls;
using MetroSet_UI.Forms;

namespace P5RStringEditor
{
    partial class MainForm : MetroSetForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ContextMenuStrip_RightClick = new ContextMenuStrip(components);
            renameToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            columnHeader = new ColumnHeader();
            menuStrip_Main = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            toggleThemeToolStripMenuItem = new ToolStripMenuItem();
            splitContainer_Main = new SplitContainer();
            listBox_Main = new ListBox();
            panel_Editor = new Panel();
            tlp_Editor = new TableLayoutPanel();
            txt_Description = new TextBox();
            lbl_Id = new Label();
            num_Id = new NumericUpDown();
            label1 = new Label();
            txt_OldName = new TextBox();
            txt_Name = new TextBox();
            lbl_Tbl = new Label();
            lbl_Name = new Label();
            lbl_OldName = new Label();
            comboBox_TBL = new ComboBox();
            ContextMenuStrip_RightClick.SuspendLayout();
            menuStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_Main).BeginInit();
            splitContainer_Main.Panel1.SuspendLayout();
            splitContainer_Main.Panel2.SuspendLayout();
            splitContainer_Main.SuspendLayout();
            panel_Editor.SuspendLayout();
            tlp_Editor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_Id).BeginInit();
            SuspendLayout();
            // 
            // ContextMenuStrip_RightClick
            // 
            ContextMenuStrip_RightClick.ImageScalingSize = new Size(20, 20);
            ContextMenuStrip_RightClick.Items.AddRange(new ToolStripItem[] { renameToolStripMenuItem, deleteToolStripMenuItem, addToolStripMenuItem });
            ContextMenuStrip_RightClick.Name = "ContextMenuStrip_RightClick";
            ContextMenuStrip_RightClick.Size = new Size(133, 76);
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new Size(132, 24);
            renameToolStripMenuItem.Text = "Rename";
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(132, 24);
            deleteToolStripMenuItem.Text = "Delete";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(132, 24);
            addToolStripMenuItem.Text = "Add";
            addToolStripMenuItem.Click += Add_Click;
            // 
            // columnHeader
            // 
            columnHeader.Width = 100;
            // 
            // menuStrip_Main
            // 
            menuStrip_Main.ImageScalingSize = new Size(20, 20);
            menuStrip_Main.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toggleThemeToolStripMenuItem });
            menuStrip_Main.Location = new Point(2, 0);
            menuStrip_Main.Name = "menuStrip_Main";
            menuStrip_Main.Padding = new Padding(3, 2, 0, 2);
            menuStrip_Main.Size = new Size(696, 28);
            menuStrip_Main.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(125, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += Save_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(125, 26);
            loadToolStripMenuItem.Text = "Load";
            // 
            // toggleThemeToolStripMenuItem
            // 
            toggleThemeToolStripMenuItem.Name = "toggleThemeToolStripMenuItem";
            toggleThemeToolStripMenuItem.Size = new Size(118, 24);
            toggleThemeToolStripMenuItem.Text = "Toggle Theme";
            toggleThemeToolStripMenuItem.Click += ToggleTheme_Click;
            // 
            // splitContainer_Main
            // 
            splitContainer_Main.Dock = DockStyle.Fill;
            splitContainer_Main.Location = new Point(2, 28);
            splitContainer_Main.Name = "splitContainer_Main";
            // 
            // splitContainer_Main.Panel1
            // 
            splitContainer_Main.Panel1.Controls.Add(listBox_Main);
            // 
            // splitContainer_Main.Panel2
            // 
            splitContainer_Main.Panel2.Controls.Add(panel_Editor);
            splitContainer_Main.Size = new Size(696, 470);
            splitContainer_Main.SplitterDistance = 232;
            splitContainer_Main.TabIndex = 3;
            // 
            // listBox_Main
            // 
            listBox_Main.BorderStyle = BorderStyle.FixedSingle;
            listBox_Main.ContextMenuStrip = ContextMenuStrip_RightClick;
            listBox_Main.Dock = DockStyle.Fill;
            listBox_Main.ItemHeight = 20;
            listBox_Main.Location = new Point(0, 0);
            listBox_Main.Name = "listBox_Main";
            listBox_Main.Size = new Size(232, 470);
            listBox_Main.TabIndex = 0;
            listBox_Main.SelectedIndexChanged += SelectedEntry_Changed;
            // 
            // panel_Editor
            // 
            panel_Editor.AutoSize = true;
            panel_Editor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Editor.Controls.Add(tlp_Editor);
            panel_Editor.Dock = DockStyle.Fill;
            panel_Editor.Location = new Point(0, 0);
            panel_Editor.Name = "panel_Editor";
            panel_Editor.Size = new Size(460, 470);
            panel_Editor.TabIndex = 0;
            // 
            // tlp_Editor
            // 
            tlp_Editor.AutoScroll = true;
            tlp_Editor.ColumnCount = 2;
            tlp_Editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlp_Editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tlp_Editor.Controls.Add(txt_Description, 1, 4);
            tlp_Editor.Controls.Add(lbl_Id, 0, 0);
            tlp_Editor.Controls.Add(num_Id, 1, 0);
            tlp_Editor.Controls.Add(label1, 0, 4);
            tlp_Editor.Controls.Add(txt_OldName, 1, 3);
            tlp_Editor.Controls.Add(txt_Name, 1, 2);
            tlp_Editor.Controls.Add(lbl_Tbl, 0, 1);
            tlp_Editor.Controls.Add(lbl_Name, 0, 2);
            tlp_Editor.Controls.Add(lbl_OldName, 0, 3);
            tlp_Editor.Controls.Add(comboBox_TBL, 1, 1);
            tlp_Editor.Dock = DockStyle.Top;
            tlp_Editor.Location = new Point(0, 0);
            tlp_Editor.Name = "tlp_Editor";
            tlp_Editor.RowCount = 5;
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.Size = new Size(460, 464);
            tlp_Editor.TabIndex = 0;
            // 
            // txt_Description
            // 
            txt_Description.Enabled = false;
            txt_Description.Location = new Point(95, 163);
            txt_Description.Multiline = true;
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(362, 213);
            txt_Description.TabIndex = 9;
            txt_Description.TextChanged += Desc_Changed;
            // 
            // lbl_Id
            // 
            lbl_Id.Anchor = AnchorStyles.Right;
            lbl_Id.AutoSize = true;
            lbl_Id.Location = new Point(58, 10);
            lbl_Id.Name = "lbl_Id";
            lbl_Id.Size = new Size(31, 20);
            lbl_Id.TabIndex = 0;
            lbl_Id.Text = "ID:";
            // 
            // num_Id
            // 
            num_Id.Anchor = AnchorStyles.Left;
            num_Id.Enabled = false;
            num_Id.Location = new Point(95, 7);
            num_Id.Name = "num_Id";
            num_Id.Size = new Size(150, 26);
            num_Id.TabIndex = 1;
            num_Id.ValueChanged += Id_Changed;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 160);
            label1.Name = "label1";
            label1.Size = new Size(86, 40);
            label1.TabIndex = 5;
            label1.Text = "Description:";
            // 
            // txt_OldName
            // 
            txt_OldName.Anchor = AnchorStyles.Left;
            txt_OldName.Enabled = false;
            txt_OldName.Location = new Point(95, 127);
            txt_OldName.Name = "txt_OldName";
            txt_OldName.Size = new Size(199, 26);
            txt_OldName.TabIndex = 7;
            txt_OldName.TextChanged += OldName_Changed;
            // 
            // txt_Name
            // 
            txt_Name.Anchor = AnchorStyles.Left;
            txt_Name.Enabled = false;
            txt_Name.Location = new Point(95, 87);
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(199, 26);
            txt_Name.TabIndex = 6;
            txt_Name.TextChanged += Name_Changed;
            // 
            // lbl_Tbl
            // 
            lbl_Tbl.Anchor = AnchorStyles.Right;
            lbl_Tbl.AutoSize = true;
            lbl_Tbl.Location = new Point(11, 50);
            lbl_Tbl.Name = "lbl_Tbl";
            lbl_Tbl.Size = new Size(78, 20);
            lbl_Tbl.TabIndex = 4;
            lbl_Tbl.Text = "TBL File:";
            // 
            // lbl_Name
            // 
            lbl_Name.Anchor = AnchorStyles.Right;
            lbl_Name.AutoSize = true;
            lbl_Name.Location = new Point(31, 90);
            lbl_Name.Name = "lbl_Name";
            lbl_Name.Size = new Size(58, 20);
            lbl_Name.TabIndex = 2;
            lbl_Name.Text = "Name:";
            // 
            // lbl_OldName
            // 
            lbl_OldName.Anchor = AnchorStyles.Right;
            lbl_OldName.AutoSize = true;
            lbl_OldName.Location = new Point(31, 120);
            lbl_OldName.Name = "lbl_OldName";
            lbl_OldName.Size = new Size(58, 40);
            lbl_OldName.TabIndex = 3;
            lbl_OldName.Text = "OG Name:";
            // 
            // comboBox_TBL
            // 
            comboBox_TBL.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_TBL.Enabled = false;
            comboBox_TBL.FormattingEnabled = true;
            comboBox_TBL.Items.AddRange(new object[] { "00 - Arcanas", "01 - Skills", "02 - Skills Again", "03 - Enemies", "04 - Personas", "05 - Traits", "06 - Accessories", "07 - Protectors", "08 - Consumables", "09 - Key Items", "10 - Materials", "11 - Melee Weapons", "12 - Battle Actions", "13 - Outfits", "14 - Skill Cards", "15 - Party FirstNames", "16 - Party LastNames", "17 - Confidant Names", "18 - Ranged Weapons" });
            comboBox_TBL.Location = new Point(95, 43);
            comboBox_TBL.Name = "comboBox_TBL";
            comboBox_TBL.Size = new Size(199, 28);
            comboBox_TBL.TabIndex = 10;
            comboBox_TBL.SelectedIndexChanged += TBL_Changed;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(700, 500);
            Controls.Add(splitContainer_Main);
            Controls.Add(menuStrip_Main);
            DropShadowEffect = false;
            Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Sizable;
            HeaderHeight = -40;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip_Main;
            MinimumSize = new Size(700, 500);
            Name = "MainForm";
            Opacity = 0.99D;
            Padding = new Padding(2, 0, 2, 2);
            ShowHeader = true;
            ShowLeftRect = false;
            SizeGripStyle = SizeGripStyle.Show;
            Style = MetroSet_UI.Enums.Style.Dark;
            Text = "P5RStringEditor";
            TextColor = Color.White;
            ThemeName = "MetroDark";
            ContextMenuStrip_RightClick.ResumeLayout(false);
            menuStrip_Main.ResumeLayout(false);
            menuStrip_Main.PerformLayout();
            splitContainer_Main.Panel1.ResumeLayout(false);
            splitContainer_Main.Panel2.ResumeLayout(false);
            splitContainer_Main.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_Main).EndInit();
            splitContainer_Main.ResumeLayout(false);
            panel_Editor.ResumeLayout(false);
            tlp_Editor.ResumeLayout(false);
            tlp_Editor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_Id).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip_Main;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ContextMenuStrip ContextMenuStrip_RightClick;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ColumnHeader columnHeader;
        private SplitContainer splitContainer_Main;
        private Panel panel_Editor;
        private TableLayoutPanel tlp_Editor;
        private ToolStripMenuItem addToolStripMenuItem;
        private ListBox listBox_Main;
        private Label lbl_Name;
        private Label lbl_Id;
        private NumericUpDown num_Id;
        private Label lbl_OldName;
        private Label lbl_Tbl;
        private TextBox txt_Description;
        private Label label1;
        private TextBox txt_OldName;
        private TextBox txt_Name;
        private ComboBox comboBox_TBL;
        private ToolStripMenuItem toggleThemeToolStripMenuItem;
    }
}