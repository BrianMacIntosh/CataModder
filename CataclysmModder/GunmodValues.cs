using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class GunmodValues : UserControl
    {
        public GunmodValues()
        {
            InitializeComponent();

            damageNumeric.Tag = new JsonFormTag(
                "damage_modifier",
                "This mod alters the damage of the affected weapon by this amount.");
            loudnessNumeric.Tag = new JsonFormTag(
                "loudness_modifier",
                "This mod alters the noise emitted by the affected weapon by this amount.");
            dispersionNumeric.Tag = new JsonFormTag(
                "dispersion_modifier",
                "This mod alters the dispersion of shots from the affected weapon by this amount. It is measured in quarter-degrees.");
            recoilNumeric.Tag = new JsonFormTag(
                "recoil_modifier",
                "This mod alters the recoil amount of the affected weapon by this amount.");
            burstNumeric.Tag = new JsonFormTag(
                "burst_modifier",
                "This mod alters the number of rounds in a burst fire by this amount.");
            clipsizeNumeric.Tag = new JsonFormTag(
                "clip_size_modifier",
                "This mod increases the clip size of the affected weapon by the specified percentage.");
            ammoComboBox.Tag = new JsonFormTag(
                "ammo_modifier",
                "This mod changes the ammo type used by the affected weapon to this type.");
            ammoCheckedListBox.Tag = new JsonFormTag(
                "acceptable_ammo",
                "If set, this mod can only be applied to guns using one of the specified ammo types.");
            typesCheckedListBox.Tag = new JsonFormTag(
                "mod_targets",
                "This mod can only be equipped to weapon types specified here.");

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);
        }
    }
}
