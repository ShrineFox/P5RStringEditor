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
using static System.Collections.Specialized.BitVector32;

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
            comboBox_Encoding.SelectedIndex = 0;
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
            FormTblSections = newSections.OrderBy(x => Array.IndexOf(NameTBLEditor.TblNamesR, x.SectionName)).ToList();
        }

        private void ImportMSGData()
        {
            foreach (var tblSection in FormTblSections.Where(x => TblSectionDatNamePairs.Any(y => y.Key == x.SectionName)))
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
            string outPath = Path.GetFullPath(".//Output//p5r.tblmod//P5REssentials//CPK//TBL.CPK/BATTLE/TABLE/NAME.TBL");
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            NameTBLEditor.SaveNameTBL(FormTblSections, outPath);
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

            TblSection tblSection = FormTblSections.First(x => x.SectionName.Equals(tblName));

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
                //using (FileSys.WaitForFile(msgPath)) { }
                AtlusScriptCompiler.Program.IsActionAssigned = false;
                AtlusScriptCompiler.Program.InputFilePath = msgPath;
                AtlusScriptCompiler.Program.OutputFilePath = outPath;
                AtlusScriptCompiler.Program.Logger = new Logger($"{nameof(AtlusScriptCompiler)}_{Path.GetFileNameWithoutExtension(outPath)}");
                AtlusScriptCompiler.Program.Listener = new FileAndConsoleLogListener(true, LogLevel.Info | LogLevel.Warning | LogLevel.Error | LogLevel.Fatal);

                AtlusScriptCompiler.Program.Main(new string[] { msgPath,
                    "-Compile", "-Library", "P5R", "-Encoding", "P5R", "-OutFormat", "V1BE", "-Out", outPath });

                //using (FileSys.WaitForFile(outPath)) { }
                File.Delete(msgPath);
            }
        }

        List<TblSection> FormTblSections = new List<TblSection>();
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
        
    }
}