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

        public GenericItemValues GenericItemControl;
        public GunmodValues GunModControl;
        public ComestibleValues ComestibleControl;
        public GunValues GunControl;
        public ToolValues ToolControl;
        public AmmoValues AmmoControl;
        public ArmorValues ArmorControl;
        public BookValues BookControl;
        public ContainValues ContainControl;

        public ItemGroupValues ItemGroupControl;

        Point itemExtensionLocation;
        Point mainPanelLocation;


        public Form1()
        {
            Instance = this;

            InitializeComponent();

            mainPanelLocation = new Point(150, 20);

            GenericItemControl = new GenericItemValues();
            GenericItemControl.Location = mainPanelLocation;
            GenericItemControl.Visible = false;
            Controls.Add(GenericItemControl);

            itemExtensionLocation = new Point(150, GenericItemControl.Bottom);

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

            ItemGroupControl = new ItemGroupValues();
            ItemGroupControl.Location = mainPanelLocation;
            ItemGroupControl.Visible = false;
            Controls.Add(ItemGroupControl);

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

        public void saveFiles()
        {
            Storage.SaveCurrentFile();
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
                    saveFiles();
            }
            return true;
        }

        private void filesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Storage.FilesLoaded) return;

            Storage.LoadFile((string)filesComboBox.SelectedItem);

            //Hide all forms
            ItemGroupControl.Visible = false;
            GenericItemControl.Visible = false;
            HideItemExtensions();

            //Show appropriate forms
            if (Storage.CurrentFileIsItems)
            {
                WinformsUtil.ControlsResetValues(GenericItemControl.Controls[0]);
                GenericItemControl.Visible = true;
            }
            else if (Path.GetFileName(Storage.CurrentFileName).Equals("item_groups.json"))
            {
                ItemGroupControl.Visible = true;
            }

            //Populate item box
            entriesListBox.ClearSelected();
            entriesListBox.DataSource = Storage.OpenItems;
            entriesListBox.DisplayMember = "Display";
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

            //Load up an item to edit
            int loadItem = entriesListBox.SelectedIndex;

            HideItemExtensions();

            Storage.LoadItem(loadItem);
        }

        public void SetHelpText(string text)
        {
            helpTextStatusLabel.Text = text;
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            Storage.OpenItems.Add(new ItemDataWrapper());
            Storage.FileChanged();
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
            Storage.SaveCurrentFile();
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.SaveCurrentFile();
        }

        private void saveItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }


    public class ItemExtensionFormTag
    {
        public string itemType;

        public ItemExtensionFormTag(string itemType)
        {
            this.itemType = itemType;
        }
    }
}
