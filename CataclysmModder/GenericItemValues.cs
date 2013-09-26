using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace CataclysmModder
{
    public partial class GenericItemValues : UserControl
    {
        public GenericItemValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string ID for the object, used in recipes and the code.");
            symbolTextBox.Tag = new JsonFormTag(
                "symbol",
                "A single character used to display this item on the map.",
                true,
                "_");
            typeComboBox.Tag = new JsonFormTag(
                "type",
                "The type of the object, controlling what extra information it needs.",
                false);
            colorComboBox.Tag = new JsonFormTag(
                 "color",
                 "The color used to display this item on the map.",
                 true,
                 "light_gray");
            nameTextBox.Tag = new JsonFormTag(
                "name",
                "The displayed name of the object. Should be all lowercase except in special cases (like proper nouns and acronyms).");
            materialsCheckedListBox.Tag = new JsonFormTag(
                "material",
                "The materials making up this item (max 2).",
                false);
            ((JsonFormTag)materialsCheckedListBox.Tag).dataSource = JsonFormTag.DataSourceType.MATERIALS;
            descriptionTextBox.Tag = new JsonFormTag(
                "description",
                "The displayed description of the item.");
            phaseComboBox.Tag = new JsonFormTag(
                "phase",
                "The default phase of matter of the object. The only effect is that liquids are drinkable and require containers.",
                false);
            priceNumeric.Tag = new JsonFormTag(
                "price",
                "The monetary value of the object.");
            volumeNumeric.Tag = new JsonFormTag(
                "volume",
                "The inventory volume taken up by this object.");
            weightNumeric.Tag = new JsonFormTag(
                "weight",
                "How much this object weighs.");
            bashNumeric.Tag = new JsonFormTag(
                "bashing",
                "Bonus bash damage from this object when wielded as a weapon.");
            cutNumeric.Tag = new JsonFormTag(
                "cutting",
                "Bonus cut damage from this object when wielded as a weapon.");
            tohitNumeric.Tag = new JsonFormTag(
                "to_hit",
                "To-hit modifier for this object when wielded as a weapon.");
            techniquesCheckedListBox.Tag = new JsonFormTag(
                "techniques",
                "Technique flags for this item when used as a weapon.",
                false);
            ((JsonFormTag)techniquesCheckedListBox.Tag).dataSource = JsonFormTag.DataSourceType.TECHNIQUES;
            flagsCheckedListBox.Tag = new JsonFormTag(
                "flags",
                "Special flags giving specific functionality to the object.",
                false);
            ((JsonFormTag)flagsCheckedListBox.Tag).dataSource = JsonFormTag.DataSourceType.FLAGS;
            functionComboBox.Tag = new JsonFormTag(
                "use_action",
                "A game function to call when the item is applied or used.",
                false);

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);

            //TODO: warning on load if symbol length > 1
        }

        private void typeComboBox_TextChanged(object sender, EventArgs e)
        {
            //HACK:
            if (WinformsUtil.Resetting > 0 && typeComboBox.Text.Equals("")) return;

            //Swap out extra boxes
            Form1.Instance.HideItemExtensions();
            foreach (Control c in Form1.Instance.Controls)
            {
                if (c.Tag is ItemExtensionFormTag
                    && ((ItemExtensionFormTag)c.Tag).itemType.Equals(
                        typeComboBox.Text, StringComparison.InvariantCultureIgnoreCase))
                {
                    c.Visible = true;
                    WinformsUtil.ControlsLoadItem(c, Storage.CurrentItemData);
                }
            }

            //Fill defaults for missing keys
            foreach (Control c in Form1.Instance.Controls)
            {
                if (c.Tag is DataFormTag && c.Visible)
                {
                    ControlSetDefaults(c);
                }
            }
        }

        private void ControlSetDefaults(Control c)
        {
            foreach (Control d in c.Controls)
            {
                if (d.Tag is JsonFormTag)
                {
                    string key = ((JsonFormTag)d.Tag).key;
                    if (!string.IsNullOrEmpty(key)
                        && !Storage.CurrentItemData.ContainsKey(key)
                        && ((JsonFormTag)d.Tag).mandatory)
                        Storage.CurrentItemData[key] = ((JsonFormTag)d.Tag).def;
                }
                if (d.Controls.Count > 0)
                {
                    ControlSetDefaults(d);
                }
            }
        }

        private void flagsCheckedListBox_selectedIndexChanged(object sender, EventArgs e)
        {
            //Post flag-specific help
            Form1.Instance.SetHelpText(((JsonFormTag.HelpItem)flagsCheckedListBox.SelectedItem).help);
        }

        private void techniquesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: post flag-specific help
        }
    }
}
