using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ToolValues : UserControl
    {
        public ToolValues()
        {
            InitializeComponent();

            ammoComboBox.Tag = new JsonFormTag(
                "ammo",
                "The ammo type consumed by this tool.",
                true,
                "NULL");
            revertsTextBox.Tag = new JsonFormTag(
                "revert_to",
                "The item ID of the item to revert to when this item runs out of charges.",
                true,
                "null");
            ((JsonFormTag)revertsTextBox.Tag).isItemId = true;
            maxChargesNumeric.Tag = new JsonFormTag(
                "max_charges",
                "The maximum number of charges this tool can hold at once.");
            spawnChargesNumeric.Tag = new JsonFormTag(
                "initial_charges",
                "The number of charges this tool has when it is spawned.");
            turnsPerNumeric.Tag = new JsonFormTag(
                "turns_per_charge",
                "Every time this many turns elapse, the item consumes one charge.");
            useChargesNumeric.Tag = new JsonFormTag(
                "charges_per_use",
                "The number of charges this tool consumes for each use.");

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);
        }
    }
}
