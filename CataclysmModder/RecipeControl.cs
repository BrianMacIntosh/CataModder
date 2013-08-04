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
    public partial class RecipeControl : UserControl
    {
        public RecipeControl()
        {
            InitializeComponent();

            resultTextBox.Tag = new JsonFormTag(
                "result",
                "The id of the item this recipe produces.");
            ((JsonFormTag)resultTextBox.Tag).isItemId = true;
            suffixTextBox.Tag = new JsonFormTag(
                "id_suffix",
                "You need to set this to a unique value if multiple recipes produce the same item.",
                false);
            skill1ComboBox.Tag = new JsonFormTag(
                "skill_pri",
                "The main skill used in crafting this recipe.",
                false);
            skill2ComboBox.Tag = new JsonFormTag(
                "skill_sec",
                "A secondary skill used in crafting this recipe.",
                false);
            diffNumeric.Tag = new JsonFormTag(
                "difficulty",
                "The skill level required to craft this recipe.");
            timeNumeric.Tag = new JsonFormTag(
                "time",
                "The amount of time required to craft this recipe (in seconds?).");
            categoryComboBox.Tag = new JsonFormTag(
                "category",
                "The tab this recipe appears under in the crafting menu.");
            autolearnCheckBox.Tag = new JsonFormTag(
                "autolearn",
                "Is this recipe automatically learned at the appropriate skill level?",
                true,
                true);
            reversibleCheckBox.Tag = new JsonFormTag(
                "reversible",
                "Can this recipe be used to take the result item apart?",
                false,
                false);
            disLearnNumeric.Tag = new JsonFormTag(
                "decomp_learn",
                "The skill level required to learn this recipe by disassembling the result item.",
                false,
                -1);

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);

            Form1.Instance.ReloadLists += LoadLists;
        }

        private void LoadLists()
        {
            Storage.LoadCraftCategories(categoryComboBox);
            Storage.LoadSkills(skill1ComboBox);
            Storage.LoadSkills(skill2ComboBox);
        }
    }
}
