using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class Form1 : Form
    {
        public delegate void ReloadEvent();
        public ReloadEvent ReloadLists;

        public static Form1 Instance { get; private set; }

        public GunmodValues GunModControl;
        public ComestibleValues ComestibleControl;
        public GunValues GunControl;
        public ToolValues ToolControl;
        public AmmoValues AmmoControl;
        public ArmorValues ArmorControl;
        public BookValues BookControl;
        public ContainValues ContainControl;

        Point itemExtensionLocation;
        Point mainPanelLocation;


        public Form1()
        {
            Instance = this;
            Storage.InitializeFileDefs();

            InitializeComponent();

            mainPanelLocation = new Point(150, 20);

            Control itemControl = new GenericItemValues();
            itemControl.Tag = new DataFormTag();
            itemControl.Location = mainPanelLocation;
            itemControl.Visible = false;
            Controls.Add(itemControl);
            Storage.FileDefSetControl(Storage.FileType.ITEMS, itemControl);

            itemExtensionLocation = new Point(150, itemControl.Bottom);

            GunModControl = new GunmodValues();
            GunModControl.Tag = new ItemExtensionFormTag("GUNMOD");
            GunModControl.Location = itemExtensionLocation;
            Controls.Add(GunModControl);

            ComestibleControl = new ComestibleValues();
            ComestibleControl.Tag = new ItemExtensionFormTag("COMESTIBLE");
            ComestibleControl.Location = itemExtensionLocation;
            Controls.Add(ComestibleControl);

            GunControl = new GunValues();
            GunControl.Tag = new ItemExtensionFormTag("GUN");
            GunControl.Location = itemExtensionLocation;
            Controls.Add(GunControl);

            ToolControl = new ToolValues();
            ToolControl.Tag = new ItemExtensionFormTag("TOOL");
            ToolControl.Location = itemExtensionLocation;
            Controls.Add(ToolControl);

            AmmoControl = new AmmoValues();
            AmmoControl.Tag = new ItemExtensionFormTag("AMMO");
            AmmoControl.Location = itemExtensionLocation;
            Controls.Add(AmmoControl);

            ArmorControl = new ArmorValues();
            ArmorControl.Tag = new ItemExtensionFormTag("ARMOR");
            ArmorControl.Location = itemExtensionLocation;
            Controls.Add(ArmorControl);

            BookControl = new BookValues();
            BookControl.Tag = new ItemExtensionFormTag("BOOK");
            BookControl.Location = itemExtensionLocation;
            Controls.Add(BookControl);

            ContainControl = new ContainValues();
            ContainControl.Tag = new ItemExtensionFormTag("CONTAINER");
            ContainControl.Location = itemExtensionLocation;
            Controls.Add(ContainControl);

            HideItemExtensions();

            Control itemGroupControl = new ItemGroupValues();
            itemGroupControl.Tag = new DataFormTag();
            itemGroupControl.Location = mainPanelLocation;
            itemGroupControl.Visible = false;
            Controls.Add(itemGroupControl);
            Storage.FileDefSetControl(Storage.FileType.ITEM_GROUPS, itemGroupControl);

            Control recipeControl = new RecipeControl();
            recipeControl.Tag = new DataFormTag();
            recipeControl.Location = mainPanelLocation;
            recipeControl.Visible = false;
            Controls.Add(recipeControl);
            Storage.FileDefSetControl(Storage.FileType.RECIPES, recipeControl);

            //Load previous workspace
            if (File.Exists(".conf"))
            {
                StreamReader read = new StreamReader(new FileStream(".conf", FileMode.Open));
                string path = read.ReadToEnd();
                read.Close();
                loadFiles(path);
            }
        }

        private void openRawsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkSave()) return;

            //Choose a directory
            FolderBrowserDialog open = new FolderBrowserDialog();
            open.ShowNewFolderButton = false;

            //Load recognized JSON files from that directory
            if (open.ShowDialog() == DialogResult.OK)
            {
                loadFiles(open.SelectedPath);
            }
        }

        public void loadFiles(string path)
        {
            Text = "Jabberwocks! - " + path;

            //Remember path
            StreamWriter writer = new StreamWriter(new FileStream(".conf", FileMode.Create));
            writer.Write(path);
            writer.Close();

            Storage.LoadFiles(path);

            //Populate list
            filesComboBox.Items.Clear();
            filesComboBox.Items.AddRange(Storage.OpenFiles);

            //Select first
            if (Storage.OpenFiles.Length > 0)
                filesComboBox.SelectedItem = Storage.OpenFiles[0];

            if (ReloadLists != null)
                ReloadLists();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkSave()) return;
            Environment.Exit(0);
        }

        /// <summary>
        /// Check and prompt to save any unsaved changes.
        /// Returns false if the calling operation should be aborted.
        /// </summary>
        private bool checkSave()
        {
            if (Storage.UnsavedChanges)
            {
                DialogResult confirm = MessageBox.Show("Open documents have unsaved changes. Save now?", "Save Changes?", MessageBoxButtons.YesNoCancel);
                if (confirm == DialogResult.Cancel)
                    return false;
                else if (confirm == DialogResult.Yes)
                    Storage.SaveOpenFiles();
            }
            return true;
        }

        private void filesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Storage.FilesLoaded) return;

            Storage.SelectFile(filesComboBox.SelectedIndex);

            //Hide all forms
            Storage.HideAllControls();
            HideItemExtensions();

            //Show appropriate forms
            CataFile fdef = Storage.GetFileDefForCurrentFile();
            if (fdef != null)
            {
                if (fdef.control != null)
                {
                    WinformsUtil.ControlsResetValues(fdef.control);
                    fdef.control.Visible = true;
                }
            }

            //Prepare item box
            entriesListBox.ClearSelected();
            entriesListBox.DataSource = Storage.OpenItems;
            entriesListBox.DisplayMember = "Display";

            //Load first item
            HideItemExtensions();
            if (entriesListBox.Items.Count > 0)
            {
                entriesListBox.SelectedIndex = 0;
                Storage.LoadItem(entriesListBox.SelectedIndex);
            }
        }

        public void HideItemExtensions()
        {
            foreach (Control c in Controls)
                if (c.Tag is ItemExtensionFormTag)
                    c.Visible = false;
        }

        private void entriesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Storage.ItemsLoaded) return;
            if (entriesListBox.SelectedIndex == Storage.CurrentItemIndex) return;

            //Load up an item to edit
            HideItemExtensions();
            Storage.LoadItem(entriesListBox.SelectedIndex);
        }

        public void SetHelpText(string text)
        {
            helpTextStatusLabel.Text = text;
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            ItemDataWrapper newitem = new ItemDataWrapper(Storage.CurrentFileIndex);
            Storage.OpenItems.Add(newitem);
            entriesListBox.SelectedIndex = Storage.OpenItems.Count - 1;

            //Fill in default values
            foreach (Control c in Controls)
            {
                if (c.Visible && c.Tag is DataFormTag)
                {
                    ControlSetDefaults(c, newitem);
                }
            }

            Storage.FileChanged();
        }

        private void ControlSetDefaults(Control c, ItemDataWrapper newitem)
        {
            foreach (Control d in c.Controls)
            {
                if (d.Tag is JsonFormTag
                    && !string.IsNullOrEmpty(((JsonFormTag)d.Tag).key)
                    && ((JsonFormTag)d.Tag).mandatory
                    && !newitem.data.ContainsKey(((JsonFormTag)d.Tag).key))
                {
                    newitem.data[((JsonFormTag)d.Tag).key] = ((JsonFormTag)d.Tag).def;
                }
                if (d.Controls.Count > 0)
                {
                    ControlSetDefaults(d, newitem);
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (entriesListBox.SelectedIndex > 0)
            {
                Storage.OpenItems.Remove((ItemDataWrapper)entriesListBox.SelectedItem);
                Storage.FileChanged();
            }
        }

        private void duplicateButton_Click(object sender, EventArgs e)
        {
            if (entriesListBox.SelectedIndex > 0)
            {
                Storage.OpenItems.Add(new ItemDataWrapper((ItemDataWrapper)entriesListBox.SelectedItem));
                Storage.FileChanged();
                entriesListBox.SelectedIndex = Storage.OpenItems.Count - 1;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!checkSave())
            {
                e.Cancel = true;
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveOpenFiles();
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveFile(Storage.CurrentFileName);
        }

        private void saveItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            SearchSelect(searchBox.Text, false);
        }

        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
                SearchSelect(searchBox.Text, true);
        }

        private void SearchSelect(string search, bool fromcurrent)
        {
            //Look for an item that matches and select it
            for (int c = (fromcurrent ? entriesListBox.SelectedIndex+1 : 0); c < Storage.OpenItems.Count; c++)
            {
                if (Storage.OpenItems[c].Display.Contains(search)
                    || (Storage.OpenItems[c].data.ContainsKey("name")
                    && ((string)Storage.OpenItems[c].data["name"]).Contains(search)))
                {
                    entriesListBox.SelectedIndex = c;
                    break;
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void nextQuicksearchResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSelect(searchBox.Text, true);
        }

        private void nextItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entriesListBox.SelectedIndex < entriesListBox.Items.Count - 1)
                entriesListBox.SelectedIndex++;
        }

        private void previousItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entriesListBox.SelectedIndex > 0)
                entriesListBox.SelectedIndex--;
        }

        private void reloadMenuItem_Click(object sender, EventArgs e)
        {
            Storage.ReloadFiles();

            //Force reload of current item
            entriesListBox_SelectedIndexChanged(null, null);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().ShowDialog();
        }

        private void exportItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ExportItemsForm().ShowDialog();
        }
    }


    public class DataFormTag
    {

    }

    public class ItemExtensionFormTag : DataFormTag
    {
        public string itemType;

        public ItemExtensionFormTag(string itemType)
        {
            this.itemType = itemType;
        }
    }
}
