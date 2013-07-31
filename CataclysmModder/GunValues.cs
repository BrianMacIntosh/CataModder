using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class GunValues : UserControl
    {
        public GunValues()
        {
            InitializeComponent();

            ammoComboBox.Tag = new JsonFormTag(
                "ammo",
                "The type of ammo fired by this weapon.");
            skillComboBox.Tag = new JsonFormTag(
                "skill",
                "The skill used and trained when firing this weapon.");
            damageNumeric.Tag = new JsonFormTag(
                "ranged_damage",
                "Base damage dealt by this weapon.");
            rangeNumeric.Tag = new JsonFormTag(
                "range",
                "The weapon's range.");
            dispersionNumeric.Tag = new JsonFormTag(
                "dispersion",
                "Disperion or inaccuracy of the weapon.  It is measured in quarter-degrees.");
            recoilNumeric.Tag = new JsonFormTag(
                "recoil",
                "The amount of recoil inflicted by the weapon.");
            durabilityNumeric.Tag = new JsonFormTag(
                "durability",
                "The durability of the weapon.");
            burstNumeric.Tag = new JsonFormTag(
                "burst",
                "The burst fire size of the weapon.");
            clipsizeNumeric.Tag = new JsonFormTag(
                "clip_size",
                "The number of rounds in one clip for the weapon.");
            reloadTimeNumeric.Tag = new JsonFormTag(
                "reload",
                "Time it takes to reload the weapon.");

            WinformsUtil.ControlsAttachHooks(Controls[0]);

            Form1.Instance.ReloadLists += LoadGunSkills;
        }

        private void LoadGunSkills()
        {
            Storage.LoadGunSkills(skillComboBox);
        }
    }
}
