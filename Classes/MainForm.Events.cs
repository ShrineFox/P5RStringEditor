using AtlusScriptLibrary.Common.Text.Encodings;
using MetroSet_UI.Child;
using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static P5RStringEditor.FTDStringConverter;

namespace P5RStringEditor
{
    public partial class MainForm : MetroSetForm
    {
        public static bool ftdMode = false;
        public static string selectedTabName = "";

        BindingSource bindingSource_ListBox = new BindingSource();

        private void EditorType_Changed(object sender, EventArgs e)
        {
            selectedTabName = "";

            if (tabControl_EditorType.SelectedTab.Text == ".FTDs" && Ftds.Count > 0)
            {
                ftdMode = true;
                SetListBoxDataSource_ToFTD();
            }
            else
            {
                ftdMode = false;
                SetListBoxDataSource_ToTBL();
            }

            tabControl_TblSections.SelectedIndex = -1;
            tabControl_TblSections.SelectedIndex = 0;
            tabControl_TblSections.SelectedTab = tabControl_TblSections.TabPages[0];
            selectedTabName = tabControl_TblSections.SelectedTab.Text;
        }

        private void SetListBoxDataSource_ToFTD()
        {
            tabControl_EditorType.SelectedIndex = 1;
            
            tabControl_TblSections.TabPages.Clear();
            foreach (var ftd in Ftds)
                tabControl_TblSections.Controls.Add(new MetroSetSetTabPage() { Text = ftd.Name, Style = Theme.ThemeStyle });
        }

        private void SetListBoxDataSource_ToTBL()
        {
            tabControl_EditorType.SelectedIndex = 0;

            if (FormTblSections.Count == 0)
                return;

            tabControl_TblSections.TabPages.Clear();
            foreach (var section in FormTblSections)
                tabControl_TblSections.Controls.Add(new MetroSetSetTabPage() { Text = section.SectionName, Style = Theme.ThemeStyle });

            tabControl_TblSections.SelectedTab = tabControl_TblSections.TabPages[0];
            selectedTabName = tabControl_TblSections.SelectedTab.Text;
        }

        private void SelectedEntry_Changed(object sender, EventArgs e)
        {
            if (ftdMode)
            {
                var ftd = Ftds.First(x => x.Name.Equals(selectedTabName));
                UpdateFormOptions_WithFTD(ftd, listBox_Main.SelectedIndex);
            }
            else
            {
                var tblEntry = (Entry)listBox_Main.SelectedItem;
                UpdateFormOptions_WithTBL(tblEntry);
            }
        }

        private void SelectedTab_Changed(object sender, EventArgs e)
        {
            if (!tabControl_TblSections.Enabled || tabControl_TblSections.SelectedTab == null)
                return;

            selectedTabName = tabControl_TblSections.SelectedTab.Text;

            if (ftdMode)
            {
                if (!Ftds.Any(x => x.Name.Equals(selectedTabName)))
                    return;

                bindingSource_ListBox.DataSource = Ftds.First(x => x.Name.Equals(selectedTabName)).Lines;
            }
            else
            {
                if (!FormTblSections.Any(x => x.SectionName.Equals(selectedTabName)))
                    return;

                bindingSource_ListBox.DataSource = FormTblSections.First(x => x.SectionName.Equals(selectedTabName)).TblEntries;
            }

            listBox_Main.DataSource = bindingSource_ListBox;
            listBox_Main.DisplayMember = "Name";
            listBox_Main.ValueMember = "Id";
            listBox_Main.FormattingEnabled = true;
            listBox_Main.Format += ListBoxFormat;
        }

        private void ListBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            bool ftdEditMode = ftdMode;
            
            dynamic entry;
            if (!ftdEditMode)
                entry = (Entry)e.ListItem;
            else
                entry = (FTDString)e.ListItem;

            int id = entry.Id;
            string itemName = entry.Name;
            string sectionName = selectedTabName;

            if (Changes.Any(x => x.SectionName == sectionName && x.Id.Equals(id)))
                e.Value = $" * [{id}] {Changes.First(x => x.SectionName == sectionName && x.Id.Equals(id)).Name}";
            else
                e.Value = $"[{id}] {itemName}";
        }

        private void UpdateFormOptions_WithFTD(FTD ftd, int selectedIndex)
        {
            ToggleFormOptions(false);

            string name = ftd.Lines[selectedIndex].Name;

            num_Id.Value = selectedIndex;
            txt_Name.Text = name;
            txt_OldName.Text = name;
            txt_Description.Text = "";

            if (Changes.Any(x => x.SectionName == ftd.Name && x.Id.Equals(selectedIndex)))
            {
                Change changedEntry = Changes.First(x => x.SectionName == ftd.Name && x.Id.Equals(selectedIndex));
                txt_Name.Text = changedEntry.Name;
            }

            ToggleFormOptions(true);
        }

        private void UpdateFormOptions_WithTBL(Entry tblEntry)
        {
            ToggleFormOptions(false);

            string name = tblEntry.Name;
            string desc = tblEntry.Description;
            string sectionName = selectedTabName;
            int id = tblEntry.Id;

            num_Id.Value = id;
            txt_Name.Text = name;
            txt_Description.Text = desc;
            txt_OldName.Text = tblEntry.Name;

            if (Changes.Any(x => x.SectionName == sectionName && x.Id.Equals(id)))
            {
                Change changedEntry = Changes.First(x => x.SectionName == sectionName && x.Id.Equals(id));
                txt_Name.Text = changedEntry.Name;
                txt_Description.Text = changedEntry.Description;
            }

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
            string jsonText = JsonConvert.SerializeObject(Changes, Newtonsoft.Json.Formatting.Indented);

            // Save to .json file
            File.WriteAllText(outPath, jsonText);
            MessageBox.Show($"Saved project file to:\n{outPath}", "Project Saved");
        }

        private void Load_Click(object sender, EventArgs e)
        {
            var filePaths = WinFormsDialogs.SelectFile("Load Project...", true, new string[] { "Project JSON (.json)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            Changes = JsonConvert.DeserializeObject<List<Change>>(File.ReadAllText(filePaths.First()));

            SetListBoxDataSource_ToTBL();

            MessageBox.Show($"Loaded changes from:\n{filePaths.First()}", "Project Loaded");
        }

        private void Name_Changed(object sender, EventArgs e)
        {
            if (!txt_Name.Enabled)
                return;

            int id = Convert.ToInt32(num_Id.Value);
            string itemName = txt_Name.Text;
            string desc = txt_Description.Text;

            if (Changes.Any(x => x.SectionName == selectedTabName && x.Id.Equals(id)))
                Changes.First(x => x.SectionName == selectedTabName && x.Id.Equals(id)).Name = itemName;
            else
                Changes.Add(new Change() { Id = id, Description = desc, Name = itemName, SectionName = selectedTabName });

            bindingSource_ListBox.ResetBindings(false);
        }

        private void Desc_Changed(object sender, EventArgs e)
        {
            if (!txt_Description.Enabled)
                return;

            string sectionName = selectedTabName;
            int id = Convert.ToInt32(num_Id.Value);
            string itemName = txt_Name.Text;
            string desc = txt_Description.Text;

            if (Changes.Any(x => x.SectionName == sectionName && x.Id.Equals(id)))
                Changes.First(x => x.SectionName == sectionName && x.Id.Equals(id))
                    .Description = desc;
            else
                Changes.Add(new Change() { Id = id, Description = desc, Name = itemName, SectionName = sectionName });
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if (outputTBLToolStripMenuItem.Checked)
                CreateNameTBL();

            foreach (var pair in TblSectionDatNamePairs)
                CreateNewBMD(pair);

            foreach (var ftd in Ftds)
                CreateNewFTD(ftd);

            MessageBox.Show("Done exporting to output folder!");
        }

        private void Import_Click(object sender, EventArgs e)
        {
            var importPath = WinFormsDialogs.SelectFile("Choose File", true, new string[] { "TBL (*.tbl)", "FTD (*.ftd)" });
            if (importPath.Count() <= 0 || !File.Exists(importPath.First()))
                return;

            switch (Path.GetExtension(importPath.First()).ToLower())
            {
                case ".tbl":
                    var bmdPath = WinFormsDialogs.SelectFolder("Choose DATMSG.PAK Folder Containing .MSGs");
                    
                    if (!Directory.Exists(bmdPath))
                        return;

                    if (!WinFormsDialogs.ShowMessageBox("Confirm Import",
                        "Are you sure you want to import? Current form data will be lost.", MessageBoxButtons.YesNo))
                        return;

                    ImportTBLData(importPath.First());
                    ImportMSGData(bmdPath, true);
                    break;
                case ".ftd":
                    ImportFTDs(importPath);
                    break;
                default:
                    break;
            }
        }

        private void Encoding_Changed(object sender, EventArgs e)
        {
            userEncoding = AtlusEncoding.GetByName(comboBox_Encoding.SelectedItem.ToString());
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            int selectedIndex = listBox_Main.SelectedIndex;
            string searchTxt = txt_Search.Text.ToLower();
            string sectionName = selectedTabName;

            if (string.IsNullOrEmpty(searchTxt))
                return;

            if (e.KeyData == Keys.Enter)
            {
                // stop windows ding noise
                e.Handled = true; 
                e.SuppressKeyPress = true;

                int i = selectedIndex + 1;
                while (i < listBox_Main.Items.Count)
                {
                    if (i == selectedIndex)
                        return;

                    var entry = (Entry)listBox_Main.Items[i];
                    if (entry.Name.ToLower().Contains(searchTxt)
                        || Changes.Where(x => x.SectionName.Equals(sectionName)).Any(x => x.Id.Equals(i) && x.Name.ToLower().Contains(searchTxt)))
                    {
                        listBox_Main.SelectedIndex = i;
                        return;
                    }

                    if (i == listBox_Main.Items.Count - 1)
                        i = 0;
                    else
                        i++;
                }
            }
        }

        private void ListBox_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && (e.Modifiers == Keys.Control || e.Modifiers == Keys.LControlKey))
            {
                txt_Search.Select();
            }
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

        private void SetLogging()
        {
            Output.Logging = true;
            Output.LogPath = "Log.txt";
            Output.LogToFile = true;
        }

        private static int GetItemIdFromFlowscriptLine(string line, bool isHelpBmd)
        {
            if (!isHelpBmd)
                return Convert.ToInt32(line.Split('_')[1].Replace("]", ""));

            return Convert.ToInt32(Int64.Parse(line.Split('_')[1].Replace("]", ""), System.Globalization.NumberStyles.HexNumber));
        }
    }
}
