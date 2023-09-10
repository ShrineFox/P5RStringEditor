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
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toggleThemeToolStripMenuItem = new ToolStripMenuItem();
            splitContainer_Main = new SplitContainer();
            listBox_Main = new ListBox();
            panel_Editor = new Panel();
            tlp_Editor = new TableLayoutPanel();
            txt_Description = new TextBox();
            lbl_Id = new Label();
            num_Id = new NumericUpDown();
            label1 = new Label();
            lbl_Name = new Label();
            txt_Name = new TextBox();
            lbl_OldName = new Label();
            txt_OldName = new TextBox();
            tabControl_TblSections = new MetroSetTabControl();
            tlp_Main = new TableLayoutPanel();
            ContextMenuStrip_RightClick.SuspendLayout();
            menuStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_Main).BeginInit();
            splitContainer_Main.Panel1.SuspendLayout();
            splitContainer_Main.Panel2.SuspendLayout();
            splitContainer_Main.SuspendLayout();
            panel_Editor.SuspendLayout();
            tlp_Editor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_Id).BeginInit();
            tlp_Main.SuspendLayout();
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
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, importToolStripMenuItem, exportToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(137, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += Save_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(137, 26);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += Load_Click;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(137, 26);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += Import_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(137, 26);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += Export_Click;
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
            splitContainer_Main.Location = new Point(3, 50);
            splitContainer_Main.Name = "splitContainer_Main";
            // 
            // splitContainer_Main.Panel1
            // 
            splitContainer_Main.Panel1.Controls.Add(listBox_Main);
            // 
            // splitContainer_Main.Panel2
            // 
            splitContainer_Main.Panel2.Controls.Add(panel_Editor);
            splitContainer_Main.Size = new Size(690, 417);
            splitContainer_Main.SplitterDistance = 186;
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
            listBox_Main.Size = new Size(186, 417);
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
            panel_Editor.Size = new Size(500, 417);
            panel_Editor.TabIndex = 0;
            // 
            // tlp_Editor
            // 
            tlp_Editor.AutoScroll = true;
            tlp_Editor.ColumnCount = 2;
            tlp_Editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlp_Editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tlp_Editor.Controls.Add(txt_Description, 1, 4);
            tlp_Editor.Controls.Add(lbl_Id, 0, 0);
            tlp_Editor.Controls.Add(num_Id, 1, 0);
            tlp_Editor.Controls.Add(label1, 0, 4);
            tlp_Editor.Controls.Add(lbl_Name, 0, 1);
            tlp_Editor.Controls.Add(txt_Name, 1, 1);
            tlp_Editor.Controls.Add(lbl_OldName, 0, 2);
            tlp_Editor.Controls.Add(txt_OldName, 1, 2);
            tlp_Editor.Dock = DockStyle.Top;
            tlp_Editor.Location = new Point(0, 0);
            tlp_Editor.Name = "tlp_Editor";
            tlp_Editor.RowCount = 5;
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlp_Editor.Size = new Size(500, 464);
            tlp_Editor.TabIndex = 0;
            // 
            // txt_Description
            // 
            txt_Description.Enabled = false;
            txt_Description.Location = new Point(128, 163);
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
            lbl_Id.Location = new Point(91, 10);
            lbl_Id.Name = "lbl_Id";
            lbl_Id.Size = new Size(31, 20);
            lbl_Id.TabIndex = 0;
            lbl_Id.Text = "ID:";
            // 
            // num_Id
            // 
            num_Id.Anchor = AnchorStyles.Left;
            num_Id.Enabled = false;
            num_Id.Location = new Point(128, 7);
            num_Id.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            num_Id.Name = "num_Id";
            num_Id.ReadOnly = true;
            num_Id.Size = new Size(150, 26);
            num_Id.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(22, 160);
            label1.Name = "label1";
            label1.Size = new Size(100, 20);
            label1.TabIndex = 5;
            label1.Text = "Description:";
            // 
            // lbl_Name
            // 
            lbl_Name.Anchor = AnchorStyles.Right;
            lbl_Name.AutoSize = true;
            lbl_Name.Location = new Point(64, 50);
            lbl_Name.Name = "lbl_Name";
            lbl_Name.Size = new Size(58, 20);
            lbl_Name.TabIndex = 2;
            lbl_Name.Text = "Name:";
            // 
            // txt_Name
            // 
            txt_Name.Anchor = AnchorStyles.Left;
            txt_Name.Enabled = false;
            txt_Name.Location = new Point(128, 47);
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(199, 26);
            txt_Name.TabIndex = 6;
            txt_Name.TextChanged += Name_Changed;
            // 
            // lbl_OldName
            // 
            lbl_OldName.Anchor = AnchorStyles.Right;
            lbl_OldName.AutoSize = true;
            lbl_OldName.Location = new Point(33, 90);
            lbl_OldName.Name = "lbl_OldName";
            lbl_OldName.Size = new Size(89, 20);
            lbl_OldName.TabIndex = 3;
            lbl_OldName.Text = "OG Name:";
            // 
            // txt_OldName
            // 
            txt_OldName.Anchor = AnchorStyles.Left;
            txt_OldName.Location = new Point(128, 87);
            txt_OldName.Name = "txt_OldName";
            txt_OldName.ReadOnly = true;
            txt_OldName.Size = new Size(199, 26);
            txt_OldName.TabIndex = 7;
            // 
            // tabControl_TblSections
            // 
            tabControl_TblSections.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tabControl_TblSections.AnimateTime = 200;
            tabControl_TblSections.BackgroundColor = Color.White;
            tabControl_TblSections.Dock = DockStyle.Fill;
            tabControl_TblSections.Enabled = false;
            tabControl_TblSections.IsDerivedStyle = true;
            tabControl_TblSections.ItemSize = new Size(100, 38);
            tabControl_TblSections.Location = new Point(3, 3);
            tabControl_TblSections.Name = "tabControl_TblSections";
            tabControl_TblSections.SelectedTextColor = Color.White;
            tabControl_TblSections.Size = new Size(690, 41);
            tabControl_TblSections.SizeMode = TabSizeMode.Fixed;
            tabControl_TblSections.Speed = 100;
            tabControl_TblSections.Style = MetroSet_UI.Enums.Style.Light;
            tabControl_TblSections.StyleManager = null;
            tabControl_TblSections.TabIndex = 4;
            tabControl_TblSections.TabStyle = MetroSet_UI.Enums.TabStyle.Style2;
            tabControl_TblSections.ThemeAuthor = "Narwin";
            tabControl_TblSections.ThemeName = "MetroLite";
            tabControl_TblSections.UnselectedTextColor = Color.Gray;
            tabControl_TblSections.UseAnimation = false;
            tabControl_TblSections.SelectedIndexChanged += SelectedTblSection_Changed;
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 1;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_Main.Controls.Add(splitContainer_Main, 0, 1);
            tlp_Main.Controls.Add(tabControl_TblSections, 0, 0);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(2, 28);
            tlp_Main.Margin = new Padding(0);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 2;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tlp_Main.Size = new Size(696, 470);
            tlp_Main.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(700, 500);
            Controls.Add(tlp_Main);
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
            tlp_Main.ResumeLayout(false);
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
        private TextBox txt_Description;
        private Label label1;
        private TextBox txt_OldName;
        private TextBox txt_Name;
        private ToolStripMenuItem toggleThemeToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private MetroSetTabControl tabControl_TblSections;
        private TableLayoutPanel tlp_Main;
    }
}