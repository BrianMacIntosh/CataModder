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

            //TODO: flags?

            itemIdTextBox.Tag = new JsonFormTag(
                null,
                "The string identifier for this item.");
            ((JsonFormTag)itemIdTextBox.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;
            itemListBox.Tag = new JsonFormTag(
                "items",
                "A list of items a player choosing this profession starts with.");
            ListBoxTagData listBoxData = new ListBoxTagData();
            listBoxData.backingList = items;
            listBoxData.defaultValue = "null";
            listBoxData.deleteButton = deleteItemButton;
            listBoxData.newButton = newItemButton;
            listBoxData.keyControl = itemIdTextBox;
            listBoxData.valueControl = null;
            ((JsonFormTag)itemListBox.Tag).listBoxData = listBoxData;

            skillComboBox.Tag = new JsonFormTag(
                null,
                "The string identifier for the skill used.");
            ((JsonFormTag)skillComboBox.Tag).dataSource = JsonFormTag.DataSourceType.SKILLS;
            skillLevelNumeric.Tag = new JsonFormTag(
                null,
                "The number of levels given to the specified skill.");
            skillsListBox.Tag = new JsonFormTag(
                "skills",
                "A list of skills a player choosing this profession starts with.");
            listBoxData = new ListBoxTagData();
            listBoxData.backingList = skills;
            Dictionary<string, object> newitem = new Dictionary<string, object>();
            newitem.Add("name", "null");
            newitem.Add("level", 0);
            listBoxData.defaultValue = newitem;
            listBoxData.deleteButton = deleteSkillButton;
            listBoxData.newButton = newSkillButton;
            listBoxData.keyControl = skillComboBox;
            listBoxData.valueControl = skillLevelNumeric;
            ((JsonFormTag)skillsListBox.Tag).listBoxData = listBoxData;

            addictionTypeComboBox.Tag = new JsonFormTag(
                null,
                "The string identifier for this addiction.");
            ((JsonFormTag)addictionTypeComboBox.Tag).dataSource = JsonFormTag.DataSourceType.ADDICTION_TYPES;
            addictionStrengthNumeric.Tag = new JsonFormTag(
                null,
                "The initial strength of the addiction.");
            addictionsListBox.Tag = new JsonFormTag(
                "addictions",
                "A list of addictions a player choosing this profession starts with.");
            listBoxData = new ListBoxTagData();
            listBoxData.backingList = addictions;
            newitem = new Dictionary<string, object>();
            newitem.Add("type", "null");
            newitem.Add("intensity", 0);
            listBoxData.defaultValue = newitem;
            listBoxData.deleteButton = deleteAddiction;
            listBoxData.newButton = newAddiction;
            listBoxData.keyControl = addictionTypeComboBox;
            listBoxData.valueControl = addictionStrengthNumeric;
            ((JsonFormTag)addictionsListBox.Tag).listBoxData = listBoxData;

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);
        }
    }
}
