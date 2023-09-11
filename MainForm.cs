using AtlusScriptLibrary.Common.Libraries;
using AtlusScriptLibrary.Common.Logging;
using AtlusScriptLibrary.Common.Text;
using AtlusScriptLibrary.Common.Text.Encodings;
using AtlusScriptLibrary.FlowScriptLanguage.Syntax;
using AtlusScriptLibrary.MessageScriptLanguage;
using AtlusScriptLibrary.MessageScriptLanguage.Compiler;
using AtlusScriptLibrary.MessageScriptLanguage.Decompiler;
using MetroSet_UI.Child;
using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace P5RStringEditor
{
    public partial class MainForm : MetroSetForm
    {
        public MainForm()
        {
            InitializeComponent();
            SetTabPages();
            ApplyTheme();
            SetLogging();
            MenuStripHelper.SetMenuStripIcons(MenuStripHelper.GetMenuStripIconPairs("Icons.txt"), this);

            ImportTBLData();
            ImportMSGData();

            tabControl_TblSections.Enabled = true;
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
                            int itemId = GetItemIdFromFlowscriptLine(lines[i], msgPath.Contains("Help"));

                            if (tblSection.TblEntries.Any(x => x.Id.Equals(itemId)))
                            {
                                string description = "";
                                for (int x = i + 1; x < lines.Length; x++)
                                {
                                    if (lines[x].Contains("[msg "))
                                        break;

                                    description += lines[x] + "\r\n";
                                }

                                description = description.Replace("[s]", "").Replace("[n]", "\r\n").Replace("[e]", "").TrimEnd();
                                tblSection.TblEntries.First(x => x.Id.Equals(Convert.ToInt32(itemId))).Description = description;
                            }
                        }
                    }
                }
            }
        }

        private void CreateNameTBL()
        {
            var tblSections = NameTBLEditor.ReadNameTBL(Path.GetFullPath("./Dependencies/P5RCBT/TABLE/NAME.TBL"));

            foreach (var section in tblSections)
            {
                if (TblSections.Any(x => x.SectionName == section.Name))
                {
                    var matchingSection = TblSections.First(x => x.SectionName == section.Name);
                    for (int i = 0; i < section.Lines.Count; i++)
                        section.Lines[i] = matchingSection.TblEntries[i].ItemName;
                }
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
            Directory.Delete(outDir + "\\h"); // hack to create folder with extension in name
            string outPath = Path.Combine(outDir, $"{bmdName}.bmd");

            if (!File.Exists(inPath))
                return;

            string[] oldMsgLines = File.ReadAllLines(inPath, AtlusEncoding.Persona5RoyalEFIGS);
            List<string> newMsgLines = new List<string>();

            TblSection tblSection = TblSections.First(x => x.SectionName.Equals(tblName));

            // Replace lines in msg with form data's description text
            for (int i = 0; i < oldMsgLines.Length; i++)
            {
                if (oldMsgLines[i].StartsWith("[msg "))
                {
                    newMsgLines.Add(oldMsgLines[i]);

                    int itemId = GetItemIdFromFlowscriptLine(oldMsgLines[i], bmdName.Contains("Help"));

                    if (tblSection.TblEntries.Any(x => x.Id.Equals(itemId)))
                    {
                        string[] descLines = tblSection.TblEntries.First(x => x.Id.Equals(itemId)).Description.Split('\n');
                        foreach (var line in descLines)
                        {
                            if (!string.IsNullOrEmpty(line) && !line.Equals("\r"))
                                newMsgLines.Add(line.Replace("\r","[n]"));
                        }
                    }

                    newMsgLines.Add("[e]");
                }
            }

            // Save new .msg to output folder
            string msgPath = outPath.Replace(".bmd", ".msg");
            File.WriteAllLines(msgPath, newMsgLines);

            if (outputBMDToolStripMenuItem.Checked)
            {
                using (FileSys.WaitForFile(msgPath)) { }
                AtlusScriptCompiler.Program.Main(new string[] { msgPath,
                "-Compile", "-Library", "P5R", "-Encoding", "P5R", "-OutFormat", "V1BE", "-Out", outPath });
                AtlusScriptCompiler.Program.IsActionAssigned = false;
                
                using (FileSys.WaitForFile(outPath)) { }
                using (FileSys.WaitForFile(msgPath)) { }
                File.Delete(msgPath);
            }
        }

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
    }
}