using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ProfessionValues : UserControl
    {
        private BindingList<ItemGroupLine> items = new BindingList<ItemGroupLine>();
        private BindingList<ItemGroupLine> skills = new BindingList<ItemGroupLine>();
        private BindingList<ItemGroupLine> addictions = new BindingList<ItemGroupLine>();

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
                null,
                "A list of items a player choosing this profession starts with.");
            itemIdTextBox.Tag = new JsonFormTag(
                null,
                "The string identifier for this item.");
            ((JsonFormTag)itemIdTextBox.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;
            skillsListBox.Tag = new JsonFormTag(
                null,
                "A list of skills a player choosing this profession starts with.");
            skillComboBox.Tag = new JsonFormTag(
                null,
                "The string identifier for the skill used.");
            skillLevelNumeric.Tag = new JsonFormTag(
                null,
                "The number of levels given to the specified skill.");
            addictionsListBox.Tag = new JsonFormTag(
                null,
                "A list of addictions a player choosing this profession starts with.");
            addictionTypeComboBox.Tag = new JsonFormTag(
                null,
                "The string identifier for this addiction.");
            addictionStrengthNumeric.Tag = new JsonFormTag(
                null,
                "The initial strength of the addiction.");

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);

            itemListBox.DataSource = items;
            itemListBox.DisplayMember = "Display";

            skillsListBox.DataSource = skills;
            skillsListBox.DisplayMember = "Display";

            addictionsListBox.DataSource = addictions;
            addictionsListBox.DisplayMember = "Display";
        }
    }
}
