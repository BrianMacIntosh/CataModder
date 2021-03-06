﻿using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class AmmoValues : UserControl
    {
        public AmmoValues()
        {
            InitializeComponent();

            ammoComboBox.Tag = new JsonFormTag(
                "ammo_type",
                "The type classification of this ammo.");
            countNumeric.Tag = new JsonFormTag(
                "count",
                "The size of spawned stacks of this ammo.");
            dispersionNumeric.Tag = new JsonFormTag(
                "dispersion",
                "The dispersion contributed to attacks by this ammo.");
            recoilNumeric.Tag = new JsonFormTag(
                "recoil",
                "The recoil contributed to attacks by this ammo.");
            damageNumeric.Tag = new JsonFormTag(
                "damage",
                "The damage contributed to attacks by this ammo.");
            pierceNumeric.Tag = new JsonFormTag(
                "pierce",
                "The armor pierce rating of this ammo.");
            rangeNumeric.Tag = new JsonFormTag(
                "range",
                "The range modifier for this ammo.");
            effectsCheckedListBox.Tag = new JsonFormTag(
                "effects",
                "Special effects of this ammo.",
                false);
            ((JsonFormTag)effectsCheckedListBox.Tag).dataSource = JsonFormTag.DataSourceType.AMMO_EFFECTS;
            stackSizeNumeric.Tag = new JsonFormTag(
                "stack_size",
                "The maximum stack size for this item.",
                false);
            casingTextBox.Tag = new JsonFormTag(
                "casing",
                "Item ID of the 'casing' item this ammo drops when it's fired.",
                false);
            ((JsonFormTag)casingTextBox.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);
        }
    }
}
