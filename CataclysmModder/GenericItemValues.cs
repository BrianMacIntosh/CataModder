using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace CataclysmModder
{
    public partial class GenericItemValues : UserControl
    {
        private BindingList<JsonFormTag.HelpItem> flagsHelp;
        private BindingList<JsonFormTag.HelpItem> techniquesHelp;

        public GenericItemValues()
        {
            InitializeComponent();

            flagsHelp = new BindingList<JsonFormTag.HelpItem>();
            //flagsHelp.Add(new JsonFormTag.HelpItem("FIT", "This piece of clothing fits the player, reducing its encumberance."));
            flagsHelp.Add(new JsonFormTag.HelpItem("VARSIZE", "This clothing can have its size adjusted by the player."));
            flagsHelp.Add(new JsonFormTag.HelpItem("OVERSIZE", "This clothing goes over other clothing, ignoring some restrictions."));
            flagsHelp.Add(new JsonFormTag.HelpItem("HOOD", "This clothing gives head warmth if head is cold and player isn't wearing a helmet (not implemented)."));
            flagsHelp.Add(new JsonFormTag.HelpItem("POCKETS", "This clothing gives hand warmth if hands are cold and player isn't wielding anything."));
            flagsHelp.Add(new JsonFormTag.HelpItem("WATCH", "This clothing allows the player to know the current time."));
            flagsHelp.Add(new JsonFormTag.HelpItem("ALARM", "This item has an alarm clock feature (not implemented)."));
            flagsHelp.Add(new JsonFormTag.HelpItem("MALE_TYPICAL", "This clothing is typically worn only by males."));
            flagsHelp.Add(new JsonFormTag.HelpItem("FEMALE_TYPICAL", "This clothing is typically worn only by females."));
            flagsHelp.Add(new JsonFormTag.HelpItem("USE_EAT_VERB", "Use the 'eat' verb for this comestible, even if it's a liquid (e.g. soup)."));
            flagsHelp.Add(new JsonFormTag.HelpItem("SEALS", "This container can safely contain liquids."));
            flagsHelp.Add(new JsonFormTag.HelpItem("RIGID", "This container has hard walls (not implemented)."));
            flagsHelp.Add(new JsonFormTag.HelpItem("WATERTIGHT", "This container can hold liquids."));
            flagsHelp.Add(new JsonFormTag.HelpItem("HOT", "This item is hot."));
            flagsHelp.Add(new JsonFormTag.HelpItem("EATEN_HOT", "This item is hot when crafted."));
            flagsHelp.Add(new JsonFormTag.HelpItem("MODE_AUX", "This weapon mod grants an alternate fire mode."));
            flagsHelp.Add(new JsonFormTag.HelpItem("MODE_BURST", "This weapon or mod has a burst fire mode"));
            flagsHelp.Add(new JsonFormTag.HelpItem("STR_RELOAD", "This weapon's reload time is reduced with a high strength stat."));
            flagsHelp.Add(new JsonFormTag.HelpItem("STAB", "This weapon mod grants a stabbing attack."));
            flagsHelp.Add(new JsonFormTag.HelpItem("NO_UNLOAD", "Ammo and items loaded into this item can't be unloaded."));
            flagsHelp.Add(new JsonFormTag.HelpItem("UNARMED_WEAPON", "This is an unarmed style."));
            //flagsHelp.Add(new JsonFormTag.HelpItem("DOUBLE_AMMO", "This tool has doubled battery capacity."));
            flagsHelp.Add(new JsonFormTag.HelpItem("GRENADE", ""));
            flagsHelp.Add(new JsonFormTag.HelpItem("RELOAD_AND_SHOOT", "This gun reloads and shoots in one action (i.e. bows)."));
            flagsHelp.Add(new JsonFormTag.HelpItem("RELOAD_ONE", "This gun reloads rounds one at a time."));
            flagsHelp.Add(new JsonFormTag.HelpItem("CHARGE", "This gun needs to charge from a UPS before firing."));
            flagsHelp.Add(new JsonFormTag.HelpItem("FIRE_100", "This gun requires 100 charges to fire."));
            flagsHelp.Add(new JsonFormTag.HelpItem("BACKBLAST", "This gun has rocket-launcher backblast."));
            flagsHelp.Add(new JsonFormTag.HelpItem("USE_UPS", "This gun requires 5 charges from a UPS to fire."));
            flagsHelp.Add(new JsonFormTag.HelpItem("STR8_DRAW", "This gun requires strength 8 to fire."));
            flagsHelp.Add(new JsonFormTag.HelpItem("STR10_DRAW", "This gun requires strength 10 to fire."));
            flagsHelp.Add(new JsonFormTag.HelpItem("STR12_DRAW", "This gun requires strength 12 to fire."));
            flagsHelp.Add(new JsonFormTag.HelpItem("LIGHT_1", "This item emits light."));
            flagsHelp.Add(new JsonFormTag.HelpItem("LIGHT_2", "This item emits light."));
            flagsHelp.Add(new JsonFormTag.HelpItem("LIGHT_8", "This item emits light."));
            flagsHelp.Add(new JsonFormTag.HelpItem("LIGHT_20", "This item emits light."));
            flagsCheckedListBox.DataSource = flagsHelp;
            flagsCheckedListBox.DisplayMember = "Display";

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
            rarityNumeric.Tag = new JsonFormTag(
                "rarity",
                "The rarity of the object.  This value is not currently used.");
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
