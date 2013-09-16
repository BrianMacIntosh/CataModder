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
            ((JsonFormTag)skillComboBox.Tag).dataSource = JsonFormTag.DataSourceType.SKILLS;
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
            ((JsonFormTag)addictionTypeComboBox.Tag).dataSource = JsonFormTag.DataSourceType.ADDICTION_TYPES;
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

        private void newSkillButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> newitem = new Dictionary<string, object>();
            newitem.Add("name", "null");
            newitem.Add("level", 0);
            skills.Add(new GroupedData(newitem));
            skillsListBox.SelectedIndex = skillsListBox.Items.Count - 1;
        }

        private void deleteSkillButton_Click(object sender, EventArgs e)
        {
            if (skillsListBox.SelectedIndex >= 0)
                skills.Remove((GroupedData)skillsListBox.SelectedItem);
        }

        private void skillsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinformsUtil.Resetting++;
            if (skillsListBox.SelectedItem != null)
            {
                //Fill fields
                skillComboBox.Text = ((GroupedData)skillsListBox.SelectedItem).Id;
                skillComboBox.Enabled = true;
                skillLevelNumeric.Value = ((GroupedData)skillsListBox.SelectedItem).Value;
                skillLevelNumeric.Enabled = true;

                deleteSkillButton.Enabled = true;
            }
            else if (!changing)
            {
                skillComboBox.Text = "";
                skillComboBox.Enabled = false;
                skillLevelNumeric.Value = 0;
                skillLevelNumeric.Enabled = false;

                deleteSkillButton.Enabled = false;
            }
            WinformsUtil.Resetting--;
        }

        private void skillComboBox_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)skillsListBox.SelectedItem).Id = skillComboBox.Text;
            changing = false;
        }

        private void skillLevelNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)skillsListBox.SelectedItem).Value = (int)skillLevelNumeric.Value;
            changing = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> newitem = new Dictionary<string, object>();
            newitem.Add("type", "null");
            newitem.Add("intensity", 0);
            addictions.Add(new GroupedData(newitem));
            addictionsListBox.SelectedIndex = addictionsListBox.Items.Count - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (addictionsListBox.SelectedIndex >= 0)
                addictions.Remove((GroupedData)addictionsListBox.SelectedItem);
        }

        private void addictionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinformsUtil.Resetting++;
            if (addictionsListBox.SelectedItem != null)
            {
                //Fill fields
                addictionTypeComboBox.Text = ((GroupedData)addictionsListBox.SelectedItem).Id;
                addictionTypeComboBox.Enabled = true;
                addictionStrengthNumeric.Value = ((GroupedData)addictionsListBox.SelectedItem).Value;
                addictionStrengthNumeric.Enabled = true;

                deleteAddiction.Enabled = true;
            }
            else if (!changing)
            {
                addictionTypeComboBox.Text = "";
                addictionTypeComboBox.Enabled = false;
                addictionStrengthNumeric.Value = 0;
                addictionStrengthNumeric.Enabled = false;

                deleteAddiction.Enabled = false;
            }
            WinformsUtil.Resetting--;
        }

        private void addictionTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)addictionsListBox.SelectedItem).Id = addictionTypeComboBox.Text;
            changing = false;
        }

        private void addictionStrengthNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)addictionsListBox.SelectedItem).Value = (int)addictionStrengthNumeric.Value;
            changing = false;
        }
    }
}
