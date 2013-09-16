using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ProfessionValues : UserControl
    {
        private BindingList<GroupedData> items = new BindingList<GroupedData>();
        private BindingList<GroupedData> skills = new BindingList<GroupedData>();
        private BindingList<GroupedData> addictions = new BindingList<GroupedData>();

        public ProfessionValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "ident",
                "A unique string identifier for this profession.");
            nameTextBox.Tag = new JsonFormTag(
                "name",
                "The displayed name of this profession.");
            descTextBox.Tag = new JsonFormTag(
                "description",
                "A description, in a few sentences, of this profession and its perks.");
            pointsNumeric.Tag = new JsonFormTag(
                "points",
                "The number of character creation points this profession costs.");

            itemListBox.Tag = new JsonFormTag(
                "items",
                "A list of items a player choosing this profession starts with.");
            ((JsonFormTag)itemListBox.Tag).backingList = items;
            itemIdTextBox.Tag = new JsonFormTag(
                null,
                "The string identifier for this item.");
            ((JsonFormTag)itemIdTextBox.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;
            skillsListBox.Tag = new JsonFormTag(
                "skills",
                "A list of skills a player choosing this profession starts with.");
            ((JsonFormTag)skillsListBox.Tag).backingList = skills;
            skillComboBox.Tag = new JsonFormTag(
                null,
                "The string identifier for the skill used.");
            skillLevelNumeric.Tag = new JsonFormTag(
                null,
                "The number of levels given to the specified skill.");
            addictionsListBox.Tag = new JsonFormTag(
                "addictions",
                "A list of addictions a player choosing this profession starts with.");
            ((JsonFormTag)addictionsListBox.Tag).backingList = addictions;
            addictionTypeComboBox.Tag = new JsonFormTag(
                null,
                "The string identifier for this addiction.");
            addictionStrengthNumeric.Tag = new JsonFormTag(
                null,
                "The initial strength of the addiction.");

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            items.Add(new GroupedData("null"));
            itemListBox.SelectedIndex = itemListBox.Items.Count - 1;
        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            if (itemListBox.SelectedIndex >= 0)
                items.Remove((GroupedData)itemListBox.SelectedItem);
        }

        private void itemListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinformsUtil.Resetting++;
            if (itemListBox.SelectedItem != null)
            {
                //Fill fields
                itemIdTextBox.Text = ((GroupedData)itemListBox.SelectedItem).Id;
                itemIdTextBox.Enabled = true;

                deleteItemButton.Enabled = true;
            }
            else if (!changing)
            {
                itemIdTextBox.Text = "";
                itemIdTextBox.Enabled = false;

                deleteItemButton.Enabled = false;
            }
            WinformsUtil.Resetting--;
        }

        private bool changing = false;

        private void itemIdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)itemListBox.SelectedItem).Id = itemIdTextBox.Text;
            changing = false;
        }
    }
}
