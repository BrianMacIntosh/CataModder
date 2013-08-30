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
        private class ComponentGroupItem
        {
            public object[] data;

            public string Id
            {
                get { return (string)data[0]; }
                set { data[0] = value; }
            }
            public int Qty
            {
                get { return (int)data[1]; }
                set { data[1] = value; }
            }

            public string Display
            {
                get
                {
                    return Id + " (" + Qty + ")";
                }
            }

            public ComponentGroupItem(object[] data)
            {
                this.data = data;
            }
        }

        private class ComponentGroup
        {
            public BindingList<ComponentGroupItem> items = new BindingList<ComponentGroupItem>();

            public bool isTools = false;

            public string Display
            {
                get
                {
                    if (items.Count == 1)
                        return items[0].Display;
                    else if (items.Count > 1)
                        return items[0].Display + " etc.";
                    else
                        return "empty";
                }
            }

            public ComponentGroup()
            {

            }

            public ComponentGroup(object[] inItems)
            {
                foreach (object[] data in inItems)
                    items.Add(new ComponentGroupItem(data));
            }

            public void AddNew()
            {
                items.Add(new ComponentGroupItem(new object[] { "null", 1 }));
            }
        }

        private ComponentGroup SelectedGroup
        {
            get
            {
                if (toolsListBox.SelectedItem != null)
                    return (ComponentGroup)toolsListBox.SelectedItem;
                else if (componentsListBox.SelectedItem != null)
                    return (ComponentGroup)componentsListBox.SelectedItem;
                else
                    return null;
            }
        }

        private BindingList<ComponentGroup> toolGroups = new BindingList<ComponentGroup>();
        private BindingList<ComponentGroup> componentGroups = new BindingList<ComponentGroup>();

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
                "The skill level required to learn this recipe by disassembling the result item (-1 forbids this).",
                false,
                -1);

            //Fields that aren't saved directly
            toolsListBox.Tag = new JsonFormTag(
                null,
                "A list of tool items required to craft this recipe.");
            componentsListBox.Tag = new JsonFormTag(
                null,
                "A list of ingredients needed to craft this recipe.");
            itemsListBox.Tag = new JsonFormTag(
                null,
                "A list of interchangeable items used by this particular component or tool group.");
            itemIdTextField.Tag = new JsonFormTag(
                null,
                "The string id of this item.");
            quantityNumeric.Tag = new JsonFormTag(
                null,
                "For components, the quantity used. For tools, the number of charges used (-1 for no charges).");

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);

            toolsListBox.DataSource = toolGroups;
            toolsListBox.DisplayMember = "Display";

            componentsListBox.DataSource = componentGroups;
            componentsListBox.DisplayMember = "Display";

            Form1.Instance.ReloadLists += LoadLists;
            WinformsUtil.OnReset += Reset;
            WinformsUtil.OnLoadItem += LoadItem;
        }

        private void LoadLists()
        {
            Storage.LoadCraftCategories(categoryComboBox);
            Storage.LoadSkills(skill1ComboBox);
            Storage.LoadSkills(skill2ComboBox);
        }

        private void Reset()
        {
            WinformsUtil.Resetting++;
            itemIdTextField.Text = "";
            quantityNumeric.Value = 0;
            toolGroups.Clear();
            componentGroups.Clear();
            itemsListBox.DataSource = null;
            WinformsUtil.Resetting--;
        }

        private void LoadItem(object item)
        {
            Dictionary<string, object> dict = (Dictionary<string, object>)item;

            //Load tools
            toolGroups.Clear();
            if (dict.ContainsKey("tools"))
                foreach (object[] data in (object[])dict["tools"])
                    toolGroups.Add(new ComponentGroup(data));

            //Load components
            componentGroups.Clear();
            if (dict.ContainsKey("components"))
                foreach (object[] data in (object[])dict["components"])
                    componentGroups.Add(new ComponentGroup(data));

            //Select none
            toolsListBox.SelectedIndex = -1;
            componentsListBox.SelectedIndex = -1;
        }

        private void toolsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolsListBox.SelectedItem != null)
            {
                componentsListBox.SelectedIndex = -1;
                itemsListBox.SelectedIndex = -1;

                itemsListBox.DataSource = ((ComponentGroup)toolsListBox.SelectedItem).items;
                itemsListBox.DisplayMember = "Display";
            }
        }

        private void componentsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (componentsListBox.SelectedItem != null)
            {
                toolsListBox.SelectedIndex = -1;
                itemsListBox.SelectedIndex = -1;

                itemsListBox.DataSource = ((ComponentGroup)componentsListBox.SelectedItem).items;
                itemsListBox.DisplayMember = "Display";
            }
        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedItem != null)
            {
                itemIdTextField.Enabled = true;
                quantityNumeric.Enabled = true;
                itemIdTextField.Text = ((ComponentGroupItem)itemsListBox.SelectedItem).Id;
                quantityNumeric.Value = ((ComponentGroupItem)itemsListBox.SelectedItem).Qty;
            }
            else
            {
                ClearItem();
            }
        }

        private void ClearItem()
        {
            itemIdTextField.Enabled = false;
            quantityNumeric.Enabled = false;
            WinformsUtil.Resetting++;
            itemIdTextField.Text = "";
            quantityNumeric.Value = 0;
            WinformsUtil.Resetting--;
        }

        private void ClearItems()
        {
            itemsListBox.DataSource = null;
        }

        private void SaveItemlistToStorage()
        {
            //TODO:
        }

        private void itemIdTextField_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            ((ComponentGroupItem)itemsListBox.SelectedItem).Id = itemIdTextField.Text;
            SaveItemlistToStorage();

            //Force displays to update
            //TODO:
        }

        private void quantityNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            ((ComponentGroupItem)itemsListBox.SelectedItem).Qty = (int)quantityNumeric.Value;
            SaveItemlistToStorage();

            //Force displays to update
            //TODO:
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            if (SelectedGroup != null)
                SelectedGroup.AddNew();
        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            if (SelectedGroup != null)
            {
                SelectedGroup.items.RemoveAt(itemsListBox.SelectedIndex);
                if (SelectedGroup.items.Count == 0)
                    ClearItem();
                itemsListBox_SelectedIndexChanged(null, null);
            }
        }

        private void newToolButton_Click(object sender, EventArgs e)
        {
            toolGroups.Add(new ComponentGroup());
            SaveItemlistToStorage();
        }

        private void deleteToolButton_Click(object sender, EventArgs e)
        {
            if (toolsListBox.SelectedItem != null)
            {
                toolGroups.Remove((ComponentGroup)toolsListBox.SelectedItem);
                if (toolGroups.Count == 0)
                {
                    ClearItem();
                    ClearItems();
                }
                toolsListBox_SelectedIndexChanged(null, null);
                SaveItemlistToStorage();
            }
        }

        private void newComponentButton_Click(object sender, EventArgs e)
        {
            componentGroups.Add(new ComponentGroup());
            SaveItemlistToStorage();
        }

        private void deleteComponentButton_Click(object sender, EventArgs e)
        {
            if (componentsListBox.SelectedItem != null)
            {
                componentGroups.Remove((ComponentGroup)componentsListBox.SelectedItem);
                if (componentGroups.Count == 0)
                {
                    ClearItem();
                    ClearItems();
                }
                componentsListBox_SelectedIndexChanged(null, null);
                SaveItemlistToStorage();
            }
        }
    }
}
