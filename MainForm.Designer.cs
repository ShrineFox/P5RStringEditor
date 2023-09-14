using MetroSet_UI.Controls;
using MetroSet_UI.Forms;
using System.Drawing;
using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputBMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputTBLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox_Encoding = new System.Windows.Forms.ToolStripComboBox();
            this.toggleThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer_Main = new System.Windows.Forms.SplitContainer();
            this.tlp_ListAndSearch = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.listBox_Main = new System.Windows.Forms.ListBox();
            this.panel_Editor = new System.Windows.Forms.Panel();
            this.tlp_Editor = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Description = new System.Windows.Forms.TextBox();
            this.lbl_Id = new System.Windows.Forms.Label();
            this.num_Id = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.lbl_OldName = new System.Windows.Forms.Label();
            this.txt_OldName = new System.Windows.Forms.TextBox();
            this.tabControl_TblSections = new MetroSet_UI.Controls.MetroSetTabControl();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).BeginInit();
            this.splitContainer_Main.Panel1.SuspendLayout();
            this.splitContainer_Main.Panel2.SuspendLayout();
            this.splitContainer_Main.SuspendLayout();
            this.tlp_ListAndSearch.SuspendLayout();
            this.panel_Editor.SuspendLayout();
            this.tlp_Editor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Id)).BeginInit();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader
            // 
            this.columnHeader.Width = 100;
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toggleThemeToolStripMenuItem});
            this.menuStrip_Main.Location = new System.Drawing.Point(2, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.menuStrip_Main.Size = new System.Drawing.Size(696, 28);
            this.menuStrip_Main.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.Save_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.Load_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.Import_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.Export_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputBMDToolStripMenuItem,
            this.outputTBLToolStripMenuItem,
            this.comboBox_Encoding});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // outputBMDToolStripMenuItem
            // 
            this.outputBMDToolStripMenuItem.Checked = true;
            this.outputBMDToolStripMenuItem.CheckOnClick = true;
            this.outputBMDToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.outputBMDToolStripMenuItem.Name = "outputBMDToolStripMenuItem";
            this.outputBMDToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.outputBMDToolStripMenuItem.Text = "Output BMD instead of MSG";
            // 
            // outputTBLToolStripMenuItem
            // 
            this.outputTBLToolStripMenuItem.Checked = true;
            this.outputTBLToolStripMenuItem.CheckOnClick = true;
            this.outputTBLToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.outputTBLToolStripMenuItem.Name = "outputTBLToolStripMenuItem";
            this.outputTBLToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.outputTBLToolStripMenuItem.Text = "Output Edited TBL";
            // 
            // comboBox_Encoding
            // 
            this.comboBox_Encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Encoding.DropDownWidth = 150;
            this.comboBox_Encoding.Items.AddRange(new object[] {
            "P5R_EFIGS",
            "P5R_JAPANESE",
            "P5R_CHINESE"});
            this.comboBox_Encoding.Name = "comboBox_Encoding";
            this.comboBox_Encoding.Size = new System.Drawing.Size(151, 28);
            // 
            // toggleThemeToolStripMenuItem
            // 
            this.toggleThemeToolStripMenuItem.Name = "toggleThemeToolStripMenuItem";
            this.toggleThemeToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.toggleThemeToolStripMenuItem.Text = "Toggle Theme";
            this.toggleThemeToolStripMenuItem.Click += new System.EventHandler(this.ToggleTheme_Click);
            // 
            // splitContainer_Main
            // 
            this.splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Main.Location = new System.Drawing.Point(3, 50);
            this.splitContainer_Main.Name = "splitContainer_Main";
            // 
            // splitContainer_Main.Panel1
            // 
            this.splitContainer_Main.Panel1.Controls.Add(this.tlp_ListAndSearch);
            // 
            // splitContainer_Main.Panel2
            // 
            this.splitContainer_Main.Panel2.Controls.Add(this.panel_Editor);
            this.splitContainer_Main.Size = new System.Drawing.Size(690, 417);
            this.splitContainer_Main.SplitterDistance = 186;
            this.splitContainer_Main.TabIndex = 3;
            // 
            // tlp_ListAndSearch
            // 
            this.tlp_ListAndSearch.ColumnCount = 1;
            this.tlp_ListAndSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_ListAndSearch.Controls.Add(this.txt_Search, 0, 0);
            this.tlp_ListAndSearch.Controls.Add(this.listBox_Main, 0, 1);
            this.tlp_ListAndSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_ListAndSearch.Location = new System.Drawing.Point(0, 0);
            this.tlp_ListAndSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_ListAndSearch.Name = "tlp_ListAndSearch";
            this.tlp_ListAndSearch.RowCount = 2;
            this.tlp_ListAndSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlp_ListAndSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tlp_ListAndSearch.Size = new System.Drawing.Size(186, 417);
            this.tlp_ListAndSearch.TabIndex = 1;
            // 
            // txt_Search
            // 
            this.txt_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Search.Location = new System.Drawing.Point(3, 3);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(180, 26);
            this.txt_Search.TabIndex = 5;
            this.txt_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            // 
            // listBox_Main
            // 
            this.listBox_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Main.ItemHeight = 20;
            this.listBox_Main.Location = new System.Drawing.Point(3, 36);
            this.listBox_Main.Name = "listBox_Main";
            this.listBox_Main.Size = new System.Drawing.Size(180, 378);
            this.listBox_Main.TabIndex = 0;
            this.listBox_Main.SelectedIndexChanged += new System.EventHandler(this.SelectedEntry_Changed);
            this.listBox_Main.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBox_Main_KeyDown);
            // 
            // panel_Editor
            // 
            this.panel_Editor.AutoSize = true;
            this.panel_Editor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_Editor.Controls.Add(this.tlp_Editor);
            this.panel_Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Editor.Location = new System.Drawing.Point(0, 0);
            this.panel_Editor.Name = "panel_Editor";
            this.panel_Editor.Size = new System.Drawing.Size(500, 417);
            this.panel_Editor.TabIndex = 0;
            // 
            // tlp_Editor
            // 
            this.tlp_Editor.AutoScroll = true;
            this.tlp_Editor.ColumnCount = 2;
            this.tlp_Editor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Editor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlp_Editor.Controls.Add(this.txt_Description, 1, 4);
            this.tlp_Editor.Controls.Add(this.lbl_Id, 0, 0);
            this.tlp_Editor.Controls.Add(this.num_Id, 1, 0);
            this.tlp_Editor.Controls.Add(this.label1, 0, 4);
            this.tlp_Editor.Controls.Add(this.lbl_Name, 0, 1);
            this.tlp_Editor.Controls.Add(this.txt_Name, 1, 1);
            this.tlp_Editor.Controls.Add(this.lbl_OldName, 0, 2);
            this.tlp_Editor.Controls.Add(this.txt_OldName, 1, 2);
            this.tlp_Editor.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_Editor.Location = new System.Drawing.Point(0, 0);
            this.tlp_Editor.Name = "tlp_Editor";
            this.tlp_Editor.RowCount = 5;
            this.tlp_Editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_Editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_Editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_Editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_Editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_Editor.Size = new System.Drawing.Size(500, 464);
            this.tlp_Editor.TabIndex = 0;
            // 
            // txt_Description
            // 
            this.txt_Description.Enabled = false;
            this.txt_Description.Location = new System.Drawing.Point(128, 163);
            this.txt_Description.Multiline = true;
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(362, 213);
            this.txt_Description.TabIndex = 9;
            this.txt_Description.TextChanged += new System.EventHandler(this.Desc_Changed);
            // 
            // lbl_Id
            // 
            this.lbl_Id.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Id.AutoSize = true;
            this.lbl_Id.Location = new System.Drawing.Point(91, 10);
            this.lbl_Id.Name = "lbl_Id";
            this.lbl_Id.Size = new System.Drawing.Size(31, 20);
            this.lbl_Id.TabIndex = 0;
            this.lbl_Id.Text = "ID:";
            // 
            // num_Id
            // 
            this.num_Id.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_Id.Enabled = false;
            this.num_Id.Location = new System.Drawing.Point(128, 7);
            this.num_Id.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.num_Id.Name = "num_Id";
            this.num_Id.ReadOnly = true;
            this.num_Id.Size = new System.Drawing.Size(150, 26);
            this.num_Id.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Description:";
            // 
            // lbl_Name
            // 
            this.lbl_Name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Location = new System.Drawing.Point(64, 50);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(58, 20);
            this.lbl_Name.TabIndex = 2;
            this.lbl_Name.Text = "Name:";
            // 
            // txt_Name
            // 
            this.txt_Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Name.Enabled = false;
            this.txt_Name.Location = new System.Drawing.Point(128, 47);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(199, 26);
            this.txt_Name.TabIndex = 6;
            this.txt_Name.TextChanged += new System.EventHandler(this.Name_Changed);
            // 
            // lbl_OldName
            // 
            this.lbl_OldName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_OldName.AutoSize = true;
            this.lbl_OldName.Location = new System.Drawing.Point(33, 90);
            this.lbl_OldName.Name = "lbl_OldName";
            this.lbl_OldName.Size = new System.Drawing.Size(89, 20);
            this.lbl_OldName.TabIndex = 3;
            this.lbl_OldName.Text = "OG Name:";
            // 
            // txt_OldName
            // 
            this.txt_OldName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_OldName.Location = new System.Drawing.Point(128, 87);
            this.txt_OldName.Name = "txt_OldName";
            this.txt_OldName.ReadOnly = true;
            this.txt_OldName.Size = new System.Drawing.Size(199, 26);
            this.txt_OldName.TabIndex = 7;
            // 
            // tabControl_TblSections
            // 
            this.tabControl_TblSections.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            this.tabControl_TblSections.AnimateTime = 200;
            this.tabControl_TblSections.BackgroundColor = System.Drawing.Color.White;
            this.tabControl_TblSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_TblSections.IsDerivedStyle = true;
            this.tabControl_TblSections.ItemSize = new System.Drawing.Size(100, 38);
            this.tabControl_TblSections.Location = new System.Drawing.Point(3, 3);
            this.tabControl_TblSections.Name = "tabControl_TblSections";
            this.tabControl_TblSections.SelectedTextColor = System.Drawing.Color.White;
            this.tabControl_TblSections.Size = new System.Drawing.Size(690, 41);
            this.tabControl_TblSections.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_TblSections.Speed = 100;
            this.tabControl_TblSections.Style = MetroSet_UI.Enums.Style.Light;
            this.tabControl_TblSections.StyleManager = null;
            this.tabControl_TblSections.TabIndex = 4;
            this.tabControl_TblSections.TabStyle = MetroSet_UI.Enums.TabStyle.Style2;
            this.tabControl_TblSections.ThemeAuthor = "Narwin";
            this.tabControl_TblSections.ThemeName = "MetroLite";
            this.tabControl_TblSections.UnselectedTextColor = System.Drawing.Color.Gray;
            this.tabControl_TblSections.UseAnimation = false;
            this.tabControl_TblSections.SelectedIndexChanged += new System.EventHandler(this.SelectedTblSection_Changed);
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.splitContainer_Main, 0, 1);
            this.tlp_Main.Controls.Add(this.tabControl_TblSections, 0, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(2, 28);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Size = new System.Drawing.Size(696, 470);
            this.tlp_Main.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.tlp_Main);
            this.Controls.Add(this.menuStrip_Main);
            this.DropShadowEffect = false;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.HeaderHeight = -40;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_Main;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.Opacity = 0.99D;
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.ShowHeader = true;
            this.ShowLeftRect = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Style = MetroSet_UI.Enums.Style.Dark;
            this.Text = "P5RStringEditor";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.splitContainer_Main.Panel1.ResumeLayout(false);
            this.splitContainer_Main.Panel2.ResumeLayout(false);
            this.splitContainer_Main.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).EndInit();
            this.splitContainer_Main.ResumeLayout(false);
            this.tlp_ListAndSearch.ResumeLayout(false);
            this.tlp_ListAndSearch.PerformLayout();
            this.panel_Editor.ResumeLayout(false);
            this.tlp_Editor.ResumeLayout(false);
            this.tlp_Editor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Id)).EndInit();
            this.tlp_Main.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip_Main;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ColumnHeader columnHeader;
        private SplitContainer splitContainer_Main;
        private Panel panel_Editor;
        private TableLayoutPanel tlp_Editor;
        private ListBox listBox_Main;
        private Label lbl_Name;
        private Label lbl_Id;
        private NumericUpDown num_Id;
        private Label lbl_OldName;
        private TextBox txt_Description;
        private Label label1;
        private TextBox txt_OldName;
        private TextBox txt_Name;
        private ToolStripMenuItem toggleThemeToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private MetroSetTabControl tabControl_TblSections;
        private TableLayoutPanel tlp_Main;
        private TextBox txt_Search;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem outputBMDToolStripMenuItem;
        private ToolStripMenuItem outputTBLToolStripMenuItem;
        private TableLayoutPanel tlp_ListAndSearch;
        private ToolStripComboBox comboBox_Encoding;
    }
}