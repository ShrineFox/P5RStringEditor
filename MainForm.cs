using AtlusScriptCompiler;
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
using static P5RStringEditor.FTDStringConverter;

namespace P5RStringEditor
{
    public partial class MainForm : MetroSetForm
    {
        public Encoding userEncoding = AtlusEncoding.Persona5RoyalEFIGS;

        public MainForm()
        {
            InitializeComponent();

            // Setup form appearance
            ApplyTheme();
            SetLogging();
            MenuStripHelper.SetMenuStripIcons(MenuStripHelper.GetMenuStripIconPairs("Icons.txt"), this);

            // Load default TBL/MSG data
            ImportTBLFromTxtFiles(TblDirPath);
            comboBox_Encoding.SelectedIndex = 0;
            ImportMSGData(DatMsgPakPath);

            // Select first tab
            SetListBoxDataSource_ToTBL();
        }

        private void ImportTBLData(string tblFilePath = "")
        {
            Changes.Clear();

            if (File.Exists(tblFilePath))
            {
                var ImportedTbl = NameTBLEditor.ReadNameTBL(tblFilePath);
                foreach (var section in ImportedTbl)
                    foreach (var entry in section.TblEntries)
                    {
                        var matchingEntry = FormTblSections.First(x => x.SectionName == section.SectionName).TblEntries.First(x => x.Id.Equals(entry.Id));
                        if (matchingEntry.Name != entry.Name)
                            Changes.Add(new Change() { Id = entry.Id, SectionName = section.SectionName, Description = matchingEntry.Description, Name = entry.Name });
                    }

                MessageBox.Show("Done importing!");
            }
            else if (Directory.Exists(tblFilePath))
            {
                ImportTBLFromTxtFiles(tblFilePath);
                MessageBox.Show("Done importing!");
            }
        }

        private void ImportTBLFromTxtFiles(string TblDirPath)
        {
            List<TblSection> newSections = new List<TblSection>();

            foreach (var txt in Directory.GetFiles(TblDirPath, "*.txt", SearchOption.TopDirectoryOnly))
            {
                string txtName = Path.GetFileNameWithoutExtension(txt);
                var section = new TblSection() { SectionName = txtName };

                string[] txtLines = File.ReadAllLines(txt);

                for (int i = 0; i < txtLines.Length; i++)
                    section.TblEntries.Add(new Entry()
                    {
                        Id = i,
                        Name = txtLines[i]
                    });
                newSections.Add(section);
            }
            FormTblSections = newSections.OrderBy(x => Array.IndexOf(NameTBLEditor.TblNamesR, x.SectionName)).ToList();
        }

        private void ImportMSGData(string DatMsgPakDir, bool useChanges = false)
        {
            if (!Directory.Exists(DatMsgPakDir))
                return;

            foreach (var tblSection in FormTblSections.Where(x => TblSectionDatNamePairs.Any(y => y.Key == x.SectionName)))
            {
                string msgPath = Path.Combine(DatMsgPakDir, $"dat{TblSectionDatNamePairs.First(x => x.Key == tblSection.SectionName).Value}Help.msg");
                if (tblSection.SectionName.Contains("Persona"))
                    msgPath = msgPath.Replace("Help.msg", ".msg");
                string bmdPath = FileSys.GetExtensionlessPath(msgPath) + ".bmd";

                
                if (File.Exists(bmdPath))
                    DecompileBMD(bmdPath);

                if (File.Exists(msgPath))
                {
                    using (FileSys.WaitForFile(msgPath)) { }

                    string[] lines = File.ReadAllText(msgPath)
                        .Replace("[s]", "").Replace("[n]", "\r\n").Replace("[e]", "")
                        .Replace("[f 0 5 65278][f 2 1]", "")
                        .Split('\n');

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith("[msg "))
                        {
                            int itemId = GetItemIdFromFlowscriptLine(lines[i], msgPath.Contains("Help") && !msgPath.Contains("AddEffect"));

                            if (tblSection.TblEntries.Any(x => x.Id.Equals(itemId)))
                            {
                                string description = "";
                                for (int x = i + 1; x < lines.Length; x++)
                                {
                                    if (lines[x].Contains("[msg "))
                                    {
                                        i = x - 1;
                                        break;
                                    }

                                    description += lines[x] + "\n";
                                }

                                description = description.TrimEnd();

                                if (useChanges)
                                {
                                    if (tblSection.TblEntries.First(x => x.Id.Equals(Convert.ToInt32(itemId))).Description != description)
                                    {
                                        if (Changes.Any(x => x.SectionName == tblSection.SectionName && x.Id == itemId))
                                            Changes.First(x => x.SectionName == tblSection.SectionName && x.Id == itemId).Description = description;
                                        else
                                            Changes.Add(new Change() { Id = itemId, SectionName = tblSection.SectionName, Description = description, Name = tblSection.TblEntries.First(x => x.Id.Equals(Convert.ToInt32(itemId))).Name });
                                    }
                                }
                                else
                                    tblSection.TblEntries.First(x => x.Id.Equals(Convert.ToInt32(itemId))).Description = description;
                            }
                        }
                    }
                }
            }
        }

        private void DecompileBMD(string bmdPath)
        {
            string outPath = FileSys.GetExtensionlessPath(bmdPath) + ".msg";

            AtlusScriptCompiler.Program.IsActionAssigned = false;
            AtlusScriptCompiler.Program.InputFilePath = bmdPath;
            AtlusScriptCompiler.Program.OutputFilePath = outPath;
            AtlusScriptCompiler.Program.MessageScriptEncoding = userEncoding;
            AtlusScriptCompiler.Program.MessageScriptTextEncodingName = userEncoding.EncodingName;
            AtlusScriptCompiler.Program.Logger = new Logger($"{nameof(AtlusScriptCompiler)}_{Path.GetFileNameWithoutExtension(outPath)}");
            AtlusScriptCompiler.Program.Listener = new FileAndConsoleLogListener(true, LogLevel.Info | LogLevel.Warning | LogLevel.Error | LogLevel.Fatal);

            AtlusScriptCompiler.Program.Main(new string[] { bmdPath,
                    "-Decompile", "-Library", "P5R", "-Encoding", comboBox_Encoding.SelectedItem.ToString(), "-Out", outPath });
        }

        private void CreateNameTBL()
        {
            string outPath = Path.GetFullPath(".//Output//p5r.tblmod//P5REssentials//CPK//TBL.CPK/BATTLE/TABLE/NAME.TBL");
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));

            // Apply form changes to TBL object
            var OutputNameTbl = FormTblSections.Copy();
            foreach (var tblSection in OutputNameTbl)
                foreach (var entry in tblSection.TblEntries)
                    if (Changes.Any(x => x.SectionName == tblSection.SectionName && x.Id == entry.Id))
                    {
                        var changedEntry = Changes.First(x => x.SectionName == tblSection.SectionName && x.Id == entry.Id);
                        entry.Name = changedEntry.Name;
                    }
            
            // Save changed TBL
            NameTBLEditor.SaveNameTBL(OutputNameTbl, outPath);
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

            List<string> newMsgLines = new List<string>();

            // Apply form changes to TBL object
            TblSection tblSection = FormTblSections.First(x => x.SectionName.Equals(tblName)).Copy();
            foreach (var entry in tblSection.TblEntries)
                if (Changes.Any(x => x.SectionName == tblSection.SectionName && x.Id == entry.Id))
                {
                    var changedEntry = Changes.First(x => x.SectionName == tblSection.SectionName && x.Id == entry.Id);
                    entry.Description = changedEntry.Description;
                }

            // Create .msg file with form data's description text
            for (int i = 0; i < tblSection.TblEntries.Count; i++)
            {
                if (bmdName.Contains("Help") && !bmdName.Contains("AddEffect"))
                    newMsgLines.Add($"[msg item_{i.ToString("X3")}]");
                else
                    newMsgLines.Add($"[msg myth_{i.ToString("D3")}]");

                string[] descLines = tblSection.TblEntries[i].Description.Split('\n');
                foreach (var line in descLines)
                    if (!string.IsNullOrEmpty(line) && !line.Equals("\r"))
                        newMsgLines.Add(line.Replace("\r", "[n]"));

                newMsgLines.Add("[e]");
                
            }

            // Save new .msg to output folder
            string msgPath = outPath.Replace(".bmd", ".msg");
            File.WriteAllLines(msgPath, newMsgLines);

            if (outputBMDToolStripMenuItem.Checked)
            {
                // HACK: Comment out mFileWriter lines in AtlusScriptCompiler/FileAndConsoleLogListener.cs
                // to prevent file access errors, at expense of no log output

                using (FileSys.WaitForFile(msgPath)) { }
                AtlusScriptCompiler.Program.IsActionAssigned = false;
                AtlusScriptCompiler.Program.InputFilePath = msgPath;
                AtlusScriptCompiler.Program.OutputFilePath = outPath;
                AtlusScriptCompiler.Program.MessageScriptEncoding = userEncoding;
                AtlusScriptCompiler.Program.MessageScriptTextEncodingName = userEncoding.EncodingName;
                AtlusScriptCompiler.Program.Logger = new Logger($"{nameof(AtlusScriptCompiler)}_{Path.GetFileNameWithoutExtension(outPath)}");
                AtlusScriptCompiler.Program.Listener = new FileAndConsoleLogListener(true, LogLevel.Info | LogLevel.Warning | LogLevel.Error | LogLevel.Fatal);

                AtlusScriptCompiler.Program.Main(new string[] { msgPath,
                    "-Compile", "-Library", "P5R", "-Encoding", comboBox_Encoding.SelectedItem.ToString(), "-OutFormat", "V1BE", "-Out", outPath });

                if (deleteOutputMSGToolStripMenuItem.Checked)
                {
                    using (FileSys.WaitForFile(msgPath)) { }
                    File.Delete(msgPath);
                }
            }
        }

        List<TblSection> FormTblSections = new List<TblSection>();
        List<FTDStringConverter.FTD> Ftds = new List<FTD>();
        List<Change> Changes = new List<Change>();
        public static string TblDirPath { get; set; } = Path.GetFullPath("./Dependencies/P5RCBT/TABLE/NAME");
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
                {"Traits", "Specific"},
                {"Materials", "Material"},
                {"Skill Cards", "SkillCard"},
                {"Outfits", "Dress"},
                {"Personas", "Myth"},
            };
    }
}