using DarkUI.Controls;
using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.Reflection;

namespace P5RStringEditor
{
    public partial class MainForm : MetroSetForm
    {
        Settings settings = new Settings();
        public class Settings
        {
            public List<NameTblEntry> nameTblEntries = new List<NameTblEntry>();
        }

        public class NameTblEntry
        {
            public string TblName { get; set; } = "";
            public int Id { get; set; } = 0;
            public string ItemName { get; set; } = "";
            public string OldName { get; set; } = "";
            public string Description { get; set; } = "";
        }

        public MainForm()
        {
            InitializeComponent();
            ApplyTheme();
            settings.nameTblEntries.Add(new NameTblEntry() { Id = 0, TblName = "13 - Outfits" });
            listBox_Main.DataBindings.Add(new Binding("Text", settings.nameTblEntries, "ItemName", 
                true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void Save_Click(object sender, EventArgs e)
        {
            // Get output path from file select prompt
            var outPaths = WinFormsDialogs.SelectFile("Save Project...", true, new string[] { "Project JSON (.json)" }, true);
            if (outPaths == null || outPaths.Count == 0 || string.IsNullOrEmpty(outPaths.First()))
                return;

            // Ensure output path ends with .json
            string outPath = outPaths.First();
            if (!outPath.ToLower().EndsWith(".json"))
                outPath += ".json";

            // Remove default values from serialized objects
            string jsonText = JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);

            // Save to .json file
            File.WriteAllText(outPath, jsonText);
            MessageBox.Show($"Saved project file to:\n{outPath}", "Preset Project Successfully");
        }

        private void Load_Click(object sender, EventArgs e)
        {
            var filePaths = WinFormsDialogs.SelectFile("Load Project...", true, new string[] { "Project JSON (.json)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filePaths.First()));
        }

        private void Add_Click(object sender, EventArgs e)
        {
            settings.nameTblEntries.Add(new NameTblEntry() { Id = 0, TblName = "13 - Outfits" });
        }

        private void SelectedEntry_Changed(object sender, EventArgs e)
        {
            var tblEntry = (NameTblEntry)listBox_Main.SelectedItem;
            UpdateFormOptions(tblEntry);
        }

        private void UpdateFormOptions(NameTblEntry tblEntry)
        {
            ToggleFormOptions(false);

            num_Id.Value = tblEntry.Id;
            txt_Name.Text = tblEntry.ItemName;
            txt_OldName.Text = tblEntry.OldName;
            txt_Description.Text = tblEntry.Description;
            comboBox_TBL.SelectedIndex = comboBox_TBL.Items.IndexOf(tblEntry.TblName);

            ToggleFormOptions(true);
        }

        private void ToggleFormOptions(bool enable)
        {
            num_Id.Enabled = enable; txt_Name.Enabled = enable;
            txt_OldName.Enabled = enable; txt_Description.Enabled = enable;
            comboBox_TBL.Enabled = enable;
        }

        private void Id_Changed(object sender, EventArgs e)
        {
            if (num_Id.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].Id = Convert.ToInt32(num_Id.Value);
        }

        private void TBL_Changed(object sender, EventArgs e)
        {
            if (comboBox_TBL.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].TblName = comboBox_TBL.SelectedItem.ToString();
        }

        private void Name_Changed(object sender, EventArgs e)
        {
            if (txt_Name.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].ItemName = txt_Name.Text;
        }

        private void OldName_Changed(object sender, EventArgs e)
        {
            if (txt_OldName.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].OldName = txt_OldName.Text;
        }

        private void Desc_Changed(object sender, EventArgs e)
        {
            if (txt_Description.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].OldName = txt_Description.Text;
        }

        private void ToggleTheme_Click(object sender, EventArgs e)
        {
            ToggleTheme();
            ApplyTheme();
        }

        private void ToggleTheme()
        {
            if (Theme.ThemeStyle == MetroSet_UI.Enums.Style.Light)
                Theme.ThemeStyle = MetroSet_UI.Enums.Style.Dark;
            else
                Theme.ThemeStyle = MetroSet_UI.Enums.Style.Light;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
            Theme.SetMenuRenderer(ContextMenuStrip_RightClick);
            Theme.RecursivelySetColors(ContextMenuStrip_RightClick);
        }
    }
}