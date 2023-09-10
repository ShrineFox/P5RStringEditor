using AtlusScriptLibrary.Common.Libraries;
using AtlusScriptLibrary.Common.Logging;
using AtlusScriptLibrary.Common.Text;
using AtlusScriptLibrary.Common.Text.Encodings;
using AtlusScriptLibrary.FlowScriptLanguage.Syntax;
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
        public static string TblPath { get; set; } = Path.GetFullPath("./Dependencies/P5RCBT/TABLE/NAME");
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
            ImportMSGData();

            tabControl_TblSections.Enabled = true;
            tabControl_TblSections.SelectedIndex = 0;

        }

        private void ImportTBLData()
        {
            List<TblSection> newSections = new List<TblSection>();
            foreach (var txt in Directory.GetFiles(TblPath, "*.txt", SearchOption.TopDirectoryOnly))
            {
                string txtName = Path.GetFileNameWithoutExtension(txt);
                var section = new TblSection() { SectionName = txtName };

                string[] txtLines = File.ReadAllLines(txt);

                for (int i = 0; i < txtLines.Length; i++)
                    section.TblEntries.Add(new Entry()
                    {
                        Id = i,
                        ItemName = txtLines[i],
                        OldName = txtLines[i]
                    });
                newSections.Add(section);
            }
            TblSections = newSections;
        }

        private void ImportMSGData()
        {
            foreach (var tblSection in TblSections.Where(x => TblSectionDatNamePairs.Any(y => y.Key == x.SectionName)))
            {
                string msgPath = Path.Combine(DatMsgPakPath, $"dat{TblSectionDatNamePairs.First(x => x.Key == tblSection.SectionName).Value}Help.msg");
                if (tblSection.SectionName.Contains("Persona"))
                    msgPath = msgPath.Replace("Help.msg", ".msg");

                if (File.Exists(msgPath))
                {
                    string[] lines = File.ReadAllLines(msgPath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith("[msg "))
                        {
                            int itemId = GetItemIdFromFlowscriptLine(lines[i]);

                            if (tblSection.TblEntries.Any(x => x.Id.Equals(itemId)))
                            {
                                string description = "";
                                for (int x = i + 1; x < lines.Length; x++)
                                {
                                    if (lines[x].Contains("[msg "))
                                        break;

                                    description += lines[x] + "\r\n";
                                }

                                description = description.Replace("[s]", "").Replace("[n]", "\r\n").Replace("[e]", "");
                                tblSection.TblEntries.First(x => x.Id.Equals(Convert.ToInt32(itemId))).Description = description;
                            }
                        }
                    }
                }
            }
        }

        private static int GetItemIdFromFlowscriptLine(string line)
        {
            return Convert.ToInt32(Int64.Parse(line.Split('_')[1].Replace("]", ""), System.Globalization.NumberStyles.HexNumber));
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
            txt_Description.Text = tblEntry.Description;
            txt_OldName.Text = tblEntry.OldName;

            ToggleFormOptions(true);
        }

        private void ToggleFormOptions(bool enable)
        {
            txt_Name.Enabled = enable;
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
            var tblSections = new List<NameTblSection>();

            foreach (var section in TblSections)
            {
                NameTblSection tblSection = new NameTblSection();
                tblSection.Name = section.SectionName;
                foreach (var entry in section.TblEntries)
                    tblSection.Lines.Add(entry.ItemName);
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

            string inPath = Path.GetFullPath($".\\Dependencies\\P5RCBT\\DATMSGPAK\\{bmdName}.msg");
            string outDir = Path.GetFullPath(".\\Output\\p5r.tblmod\\FEmulator\\PAK\\INIT\\DATMSG.PAK");
            Directory.CreateDirectory(outDir + "\\h");
            string outPath = Path.Combine(outDir, $"{bmdName}.bmd");

            if (!File.Exists(inPath))
                return;

            string[] oldMsgLines = File.ReadAllLines(inPath);
            List<string> newMsgLines = new List<string>();

            TblSection tblSection = TblSections.First(x => x.SectionName.Equals(tblName));

            // Replace lines in msg with form data's description text
            for (int i = 0; i < oldMsgLines.Length; i++)
            {
                if (oldMsgLines[i].StartsWith("[msg "))
                {
                    newMsgLines.Add(oldMsgLines[i]);

                    int itemId = GetItemIdFromFlowscriptLine(oldMsgLines[i]);

                    if (tblSection.TblEntries.Any(x => x.Id.Equals(itemId)))
                    {
                        string[] descLines = tblSection.TblEntries.First(x => x.Id.Equals(itemId)).Description.Split("\n");
                        foreach (var line in descLines)
                        {
                            newMsgLines.Add(line);
                        }
                    }

                    newMsgLines.Add("[e]");
                }
            }

            // Save new .msg to output folder
            string msgPath = outPath.Replace(".bmd", ".msg");
            File.WriteAllLines(msgPath, newMsgLines);
            using (FileSys.WaitForFile(msgPath)) { }

            // Compile new .msg to .bmd
            var compiler = new MessageScriptCompiler(FormatVersion.Version1BigEndian, AtlusEncoding.Persona5RoyalEFIGS);
            compiler.Library = LibraryLookup.GetLibrary("P5R");
            MessageScript script = null;
            bool success = compiler.TryCompile(File.OpenText(msgPath), out script);

            // Save .bmd to output folder
            if (!success)
                MessageBox.Show($"Failed to compile output bmd: {bmdName}.bmd");
            else
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
            //Theme.SetMenuRenderer(ContextMenuStrip_RightClick);
            //Theme.RecursivelySetColors(ContextMenuStrip_RightClick);
        }
    }
}