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
    public partial class ArmorValues : UserControl
    {
        public ArmorValues()
        {
            InitializeComponent();

            coversCheckedListBox.Tag = new JsonFormTag(
                "covers",
                "A list of body parts the armor protects.");
            encumberanceNumeric.Tag = new JsonFormTag(
                "encumberance",
                "The encumberance this armor inflicts on covered body parts.");
            coverageNumeric.Tag = new JsonFormTag(
                "coverage",
                "The percent chance that this armor covers any particular point in its worn area.");
            thicknessNumeric.Tag = new JsonFormTag(
                "material_thickness",
                "");
            enviProtectNumeric.Tag = new JsonFormTag(
                "environmental_protection",
                "The amount of environmental protection this armor provides when worn.");
            warmthNumeric.Tag = new JsonFormTag(
                "warmth",
                "The amount of warmth this armor provides when worn.");
            storageNumeric.Tag = new JsonFormTag(
                "storage",
                "The amount of storage volume this armor provides when worn.");
            powerArmorCheckBox.Tag = new JsonFormTag(
                "power_armor",
                "Is this armor power armor?  Power armor can only be worn with other power armor.",
                false,
                false);

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);
        }
    }
}
