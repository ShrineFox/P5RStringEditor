using AtlusScriptLibrary.Common.Libraries;
using AtlusScriptLibrary.Common.Logging;
using AtlusScriptLibrary.Common.Text;
using AtlusScriptLibrary.Common.Text.Encodings;
using AtlusScriptLibrary.MessageScriptLanguage;
using AtlusScriptLibrary.MessageScriptLanguage.Compiler;
using AtlusScriptLibrary.MessageScriptLanguage.Decompiler;
using DarkUI.Controls;
using MetroSet_UI.Child;
using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using static P5RStringEditor.MainForm;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;

namespace P5RStringEditor
{
    public partial class MainForm : MetroSetForm
    {
        List<TblSection> TblSections = new List<TblSection>();
        public static string TblPath { get; set; } = Path.GetFullPath("./Dependencies/P5RCBT/TABLE/NAME.TBL");
        public static string DatMsgPakPath { get; set; } = Path.GetFullPath("./Dependencies/P5RCBT/DATMSGPAK");
        public static Dictionary<string, string> TblSectionDatNamePairs = new Dictionary<string, string>()
            {
                {"Skills", "Skill"},
                {"Melee Weapons", "Weapon"},
                {"Ranged Weapons", "Gun"},
                {"Protectors", "Armor"},
                {"Accessories", "Accessory"},
                {"Consumables", "Item"},
                {"Key Items", "EventItem"},
                {"Materials", "Material"},
                {"Skill Cards", "SkillCard"},
                {"Outfits", "Dress"},
                {"Personas", "Myth"},
            };
        BindingSource bs = new BindingSource();

        public class TblSection
        {
            public string SectionName { get; set; } = "";
            public List<Entry> TblEntries { get; set; } = new List<Entry>();
        }

        public class Entry
        {
            public int Id { get; set; } = 0;
            public string ItemName { get; set; } = "";
            public string OldName { get; set; } = "";
            public string Description { get; set; } = "";
        }

        public MainForm()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            SetTabPages();
            ApplyTheme();

            ImportTBLData();
            ImportBMDData();

            tabControl_TblSections.Enabled = true;
            tabControl_TblSections.SelectedIndex = 0;

        }

        private void ImportTBLData()
        {
            List<TblSection> newSections = new List<TblSection>();
            foreach (var nameTblSection in NameTBLEditor.ReadNameTBL(TblPath))
            {
                var section = new TblSection() { SectionName = nameTblSection.Name };
                for (int i = 0; i < nameTblSection.Lines.Count; i++)
                    section.TblEntries.Add(new Entry()
                    {
                        Id = i,
                        ItemName = nameTblSection.Lines[i],
                        OldName = nameTblSection.Lines[i]
                    });
                newSections.Add(section);
            }
            TblSections = newSections;
        }

        private void ImportBMDData()
        {
            foreach (var tblSection in TblSections.Where(x => TblSectionDatNamePairs.Any(y => y.Key == x.SectionName)))
            {
                string datPath = Path.Combine(DatMsgPakPath, $"dat{TblSectionDatNamePairs.First(x => x.Key == tblSection.SectionName).Value}Help.bmd");
                if (tblSection.SectionName.Contains("Persona"))
                    datPath = datPath.Replace("Help.bmd", ".bmd");

                if (File.Exists(datPath))
                {
                    // Load BMD as a script
                    var script = MessageScript.FromFile(datPath, FormatVersion.Version1BigEndian, AtlusEncoding.Persona5RoyalEFIGS);
                    for (var i = 0; i < script.Dialogs.Count; i++)
                    {
                        // Get item ID from each dialog in script
                        var message = script.Dialogs[i];
                        long itemId = Int64.Parse(message.Name.Split('_')[1], System.Globalization.NumberStyles.HexNumber);
                        if (tblSection.TblEntries.Any(x => x.Id == Convert.ToInt32(itemId)))
                        {
                            // Get message contents
                        }
                    }
                }
            }
        }

        private void SetTabPages()
        {
            foreach (var pair in TblSectionDatNamePairs)
                tabControl_TblSections.Controls.Add(new MetroSetSetTabPage() { Text = pair.Key });
        }

        private void SelectedTblSection_Changed(object sender, EventArgs e)
        {
            if (!tabControl_TblSections.Enabled)
                return;

            SetListBoxDataSource();
            SelectFirstEntry();
        }

        private void SetListBoxDataSource()
        {
            bs.DataSource = TblSections.First(x => x.SectionName.Equals(tabControl_TblSections.SelectedTab.Text)).TblEntries;
            listBox_Main.DataSource = bs;
            listBox_Main.DisplayMember = "ItemName";
            listBox_Main.ValueMember = "Id";
        }

        private void SelectFirstEntry()
        {
            listBox_Main.SelectedIndex = 0;
        }

        private void SelectedEntry_Changed(object sender, EventArgs e)
        {
            var tblEntry = (Entry)listBox_Main.SelectedItem;
            UpdateFormOptions(tblEntry);
        }

        private void UpdateFormOptions(Entry tblEntry)
        {
            ToggleFormOptions(false);

            txt_Name.Text = tblEntry.ItemName;
            txt_OldName.Text = tblEntry.OldName;
            txt_Description.Text = tblEntry.Description;

            ToggleFormOptions(true);
        }

        private void ToggleFormOptions(bool enable)
        {
            txt_Name.Enabled = enable;
            txt_OldName.Enabled = enable; 
            txt_Description.Enabled = enable;
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
            string jsonText = JsonConvert.SerializeObject(TblSections, Newtonsoft.Json.Formatting.Indented);

            // Save to .json file
            File.WriteAllText(outPath, jsonText);
            MessageBox.Show($"Saved project file to:\n{outPath}", "Preset Project Successfully");
        }

        private void Load_Click(object sender, EventArgs e)
        {
            var filePaths = WinFormsDialogs.SelectFile("Load Project...", true, new string[] { "Project JSON (.json)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            TblSections = JsonConvert.DeserializeObject<List<TblSection>>(File.ReadAllText(filePaths.First()));

            SetListBoxDataSource();
            SelectFirstEntry();
        }

        private void Name_Changed(object sender, EventArgs e)
        {
            if (!txt_Name.Enabled)
                return;
            TblSections.First(x => x.SectionName.Equals(tabControl_TblSections.SelectedTab.Text)).TblEntries[listBox_Main.SelectedIndex].ItemName = txt_Name.Text;

            bs.ResetBindings(false);
        }

        private void OldName_Changed(object sender, EventArgs e)
        {
            if (!txt_OldName.Enabled)
                return;
            TblSections.First(x => x.SectionName.Equals(tabControl_TblSections.SelectedTab.Text)).TblEntries[listBox_Main.SelectedIndex].OldName = txt_OldName.Text;
        }

        private void Desc_Changed(object sender, EventArgs e)
        {
            if (!txt_Description.Enabled)
                return;
            TblSections.First(x => x.SectionName.Equals(tabControl_TblSections.SelectedTab.Text)).TblEntries[listBox_Main.SelectedIndex].Description = txt_Description.Text;
        }

        private void Export_Click(object sender, EventArgs e)
        {
            CreateNameTBL();

            foreach (var pair in TblSectionDatNamePairs)
                CreateNewBMD(pair);

            MessageBox.Show("Done exporting to output folder!");
        }

        private void CreateNameTBL()
        {
            var tblSections = NameTBLEditor.ReadNameTBL(TblPath);

            foreach (var section in TblSections)
                foreach (var entry in section.TblEntries)
                {
                    tblSections.First(x => x.Name.Equals(section.SectionName)).Lines[entry.Id] = entry.ItemName;
                }

            string outPath = Path.GetFullPath(".//Output//p5r.tblmod//P5REssentials//CPK//TBL.CPK/BATTLE/TABLE/NAME.TBL");
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            NameTBLEditor.SaveNameTBL(tblSections, outPath);
        }

        private void CreateNewBMD(KeyValuePair<string, string> tblPair)
        {
            string tblName = tblPair.Key;
            string datName = tblPair.Value;

            // Get input/output paths
            string bmdName = "dat" + datName;
            if (datName != "Myth")
                bmdName += "Help";

            string inPath = Path.GetFullPath($".\\Dependencies\\{bmdName}.bmd");
            string outDir = Path.GetFullPath(".\\Output\\p5r.tblmod\\FEmulator\\PAK\\INIT\\DATMSG.PAK");
            Directory.CreateDirectory("FEmulator\\PAK\\INIT\\DATMSG.PAK");
            string outPath = Path.Combine(outDir, $"{bmdName}.bmd");

            // Load BMD as a script
            var script = MessageScript.FromFile(inPath, FormatVersion.Version1BigEndian, AtlusEncoding.Persona5RoyalEFIGS);
            for (var i = 0; i < script.Dialogs.Count; i++)
            {
                // Get item ID from each dialog in script
                var message = script.Dialogs[i];
                long itemId = Int64.Parse(message.Name.Split('_')[1], System.Globalization.NumberStyles.HexNumber);

                {
                    // Replace description text
                    TokenTextBuilder lineBuilder = new TokenTextBuilder();
                    lineBuilder.AddFunction(0, 5, new ushort[] { 65278 });
                    lineBuilder.AddFunction(2, 1);

                    string desc = TblSections.First(x => x.SectionName == tblName)
                        .TblEntries.First(x => x.Id == Convert.ToInt32(itemId)).Description;
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
            script.ToFile(outPath);
        }

        private void Import_Click(object sender, EventArgs e)
        {
            var tblPath = WinFormsDialogs.SelectFile("Choose TBL File", false, new string[] { "TBL (*.tbl)" });
            if (tblPath.Count > 0 && File.Exists(tblPath.First()))
                TblPath = tblPath.First();

            var bmdPath = WinFormsDialogs.SelectFolder("Choose DATMSG.PAK Folder");
            if (Directory.Exists(bmdPath))
                DatMsgPakPath = bmdPath;

            if (!WinFormsDialogs.ShowMessageBox("Confirm Import",
                "Are you sure you want to import? Current form data will be lost.", MessageBoxButtons.YesNo))
                return;

            //LoadTBLEntries();
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