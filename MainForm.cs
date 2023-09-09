using AtlusScriptLibrary.Common.Libraries;
using AtlusScriptLibrary.Common.Logging;
using AtlusScriptLibrary.Common.Text.Encodings;
using AtlusScriptLibrary.MessageScriptLanguage;
using AtlusScriptLibrary.MessageScriptLanguage.Compiler;
using DarkUI.Controls;
using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.DataFormats;

namespace P5RStringEditor
{
    public partial class MainForm : MetroSetForm
    {
        Settings settings = new Settings();
        BindingSource bs = new BindingSource();
        public class Settings
        {
            public BindingList<NameTblEntry> nameTblEntries = new BindingList<NameTblEntry>();
        }

        public class NameTblEntry
        {
            public int ProgramId { get; set; } = 0;
            public string TblName { get; set; } = "";
            public int Id { get; set; } = 0;
            public string ItemName { get; set; } = "";
            public string OldName { get; set; } = "";
            public string Description { get; set; } = "";
        }

        public MainForm()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ApplyTheme();
            SetupDataSource();
        }

        private void SetupDataSource()
        {
            bs.DataSource = settings.nameTblEntries;
            listBox_Main.DataSource = bs;
            listBox_Main.DisplayMember = "ItemName";
            listBox_Main.ValueMember = "ProgramId";
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
            SetupDataSource();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            // Create unique ID for binding to listbox
            int id = 0;
            while (true)
            {
                if (settings.nameTblEntries.Any(x => x.ProgramId == id))
                    id++;
                else
                    break;
            }

            settings.nameTblEntries.Add(new NameTblEntry()
            {
                ItemName = "Untitled",
                ProgramId = id,
                Id = 0,
                TblName = "13 - Outfits"
            });
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
            if (!num_Id.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].Id = Convert.ToInt32(num_Id.Value);
        }

        private void TBL_Changed(object sender, EventArgs e)
        {
            if (!comboBox_TBL.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].TblName = comboBox_TBL.SelectedItem.ToString();
        }

        private void Name_Changed(object sender, EventArgs e)
        {
            if (!txt_Name.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].ItemName = txt_Name.Text;

            bs.ResetBindings(false);
        }

        private void OldName_Changed(object sender, EventArgs e)
        {
            if (!txt_OldName.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].OldName = txt_OldName.Text;
        }

        private void Desc_Changed(object sender, EventArgs e)
        {
            if (!txt_Description.Enabled)
                return;
            settings.nameTblEntries[listBox_Main.SelectedIndex].Description = txt_Description.Text;
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

        private void Export_Click(object sender, EventArgs e)
        {
            string outPath = Path.GetFullPath("./Output");

            foreach (var versionPath in new string[] { /* Path.Combine(outPath, "p5r.mod"), */ Path.Combine(outPath, "p5r.mod.cbt") })
            {
                //CreateNameTBL(versionPath, versionPath.Contains("cbt"));
            }

            CreateNewBMD(outPath, "01 - Skills", "Skill");
            CreateNewBMD(outPath, "11 - Melee Weapons", "Weapon");
            CreateNewBMD(outPath, "18 - Ranged Weapons", "Gun");
            CreateNewBMD(outPath, "07 - Protectors", "Armor");
            CreateNewBMD(outPath, "06 - Accessories", "Accessory");
            CreateNewBMD(outPath, "08 - Consumables", "Item");
            CreateNewBMD(outPath, "09 - Key Items", "EventItem");
            CreateNewBMD(outPath, "10 - Materials", "Material");
            CreateNewBMD(outPath, "14 - Skill Cards", "SkillCard");
            CreateNewBMD(outPath); // Dress
            CreateNewBMD(outPath, "04 - Personas", "Myth");

            MessageBox.Show("Done exporting to output folder!");
        }

        private void CreateNameTBL(string outPath, bool isCBT)
        {
            outPath = Path.Combine(outPath, "BATTLE/TABLE/NAME.TBL");

            string inPath = "./Dependencies/P5R/NAME.TBL";
            if (isCBT)
                inPath = "./Dependencies/P5RCBT/NAME.TBL";

            using (FileSys.WaitForFile(inPath)) { }

            var tblSections = NameTBLEditor.ReadNameTBL(inPath);

            foreach (var entry in settings.nameTblEntries)
            {
                if (tblSections.Any(x => x.Name.Equals(entry.TblName)))
                {
                    tblSections.First(x => x.Name.Equals(entry.TblName)).Lines[entry.Id] = entry.ItemName;
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(outPath));

            NameTBLEditor.SaveNameTBL(tblSections, outPath);
        }

        private void CreateNewBMD(string outPath, string tblName = "13 - Outfits", string datName = "Dress")
        {
            string bmdName = "dat" + datName;
            if (datName != "Myth")
                bmdName += "Help";

            // Skip this if no outfit name TBL entries, or no descriptions
            if (!settings.nameTblEntries.Any(x => x.TblName == tblName)
                || !settings.nameTblEntries.Any(x => !string.IsNullOrEmpty(x.Description)))
                return;

            // Load BMD as a script
            string inPath = Path.GetFullPath($"./Dependencies/{bmdName}.bmd");
            var script = MessageScript.FromFile(inPath, FormatVersion.Version1BigEndian, AtlusEncoding.Persona5RoyalEFIGS);
            for (var i = 0; i < script.Dialogs.Count; i++)
            {
                // Get item ID from each dialog in script
                var message = script.Dialogs[i];
                long itemId = Int64.Parse(message.Name.Split('_')[1], System.Globalization.NumberStyles.HexNumber);
                if (settings.nameTblEntries.Any(x => x.TblName == tblName 
                    && x.Id == Convert.ToInt32(itemId)))
                {
                    // If there's a message with a matching ID, replace with description text
                    TokenTextBuilder lineBuilder = new TokenTextBuilder();
                    lineBuilder.AddFunction(0, 5, new ushort[] { 65278 });
                    lineBuilder.AddFunction(2, 1);

                    string desc = settings.nameTblEntries.First(x => x.TblName == tblName 
                        && x.Id == Convert.ToInt32(itemId)).Description;
                    string[] descLines = desc.Split('\n');

                    foreach (var line in descLines)
                    {
                        lineBuilder.AddToken(new StringToken(line.Replace("\r", "")));
                        lineBuilder.AddNewLine();
                    }
                    message.Lines[0] = lineBuilder.Build();
                }
            }

            // Output edited script
            outPath = Path.Combine(outPath, $"FEmulator/PAK/INIT/DATMSG.PAK/{datName}.bmd");
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            script.ToFile(outPath);
        }
    }
}