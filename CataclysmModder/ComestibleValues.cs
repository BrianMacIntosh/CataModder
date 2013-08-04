using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ComestibleValues : UserControl
    {
        public ComestibleValues()
        {
            InitializeComponent();

            comesttypeComboBox.Tag = new JsonFormTag(
                "comestible_type",
                "Is this comestible a food, a drink, or a med?",
                true,
                "FOOD");
            toolTextBox.Tag = new JsonFormTag(
                "tool",
                "The item id of an item that is required to ingest this comestible.",
                true,
                "null");
            ((JsonFormTag)toolTextBox.Tag).isItemId = true;
            containerTextBox.Tag = new JsonFormTag(
                "container",
                "The item id of an item that will be the default container for this comestible.",
                true,
                "null");
            ((JsonFormTag)containerTextBox.Tag).isItemId = true;
            nutritionNumeric.Tag = new JsonFormTag(
                "nutrition",
                "The amount applied to the player's hunger level when this comestible is taken.");
            quenchNumeric.Tag = new JsonFormTag(
                "quench",
                "The amount applied to the player's thirst level when this comestible is taken.");
            spoilsNumeric.Tag = new JsonFormTag(
                "spoils_in",
                "The amount of time it takes for this item to rot, in hours.");
            addictionNumeric.Tag = new JsonFormTag(
                "addiction_potential",
                "The amount applied to the player's addiction level when this comestible is taken.");
            chargesNumeric.Tag = new JsonFormTag(
                "charges",
                "The number of uses in one instance of this item.",
                true,
                1);
            stimNumeric.Tag = new JsonFormTag(
                "stim",
                "The amount applied to the player's stimulus level when this comestible is taken.");
            healNumeric.Tag = new JsonFormTag(
                "heal",
                "The amount applied to the player's hidden Shealth level when this comestible is taken.");
            funNumeric.Tag = new JsonFormTag(
                "fun",
                "The amount applied to the player's morale level when this comestible is taken.");
            addictionTypeComboBox.Tag = new JsonFormTag(
                "addiction_type",
                "The type of addiction a player can get from taking this comestible.",
                true,
                "none");

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);
        }
    }
}
