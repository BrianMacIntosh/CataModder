using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class RecipeControl : UserControl
    {
        private class ComponentGroup : INotifyPropertyChanged
        {
            public BindingList<GroupedData> items = new BindingList<GroupedData>();

            public event PropertyChangedEventHandler PropertyChanged;

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

            public void NotifyItemChanged(object sender, PropertyChangedEventArgs args)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Display"));
            }

            public ComponentGroup()
            {
                AddNew();
            }

            public ComponentGroup(object[] inItems)
            {
                foreach (object[] data in inItems)
                {
                    items.Add(new GroupedData(data));
                    items[items.Count - 1].PropertyChanged += NotifyItemChanged;
                }
            }

            public void AddNew()
            {
                items.Add(new GroupedData(new object[] { "null", 0 }));
                items[items.Count - 1].PropertyChanged += NotifyItemChanged;
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

        private BindingList<GroupedData> bookGroup = new BindingList<GroupedData>();
        private BindingList<GroupedData> requireSkills = new BindingList<GroupedData>();

        public RecipeControl()
        {
            InitializeComponent();

            resultTextBox.Tag = new JsonFormTag(
                "result",
                "The id of the item this recipe produces.");
            ((JsonFormTag)resultTextBox.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;
            suffixTextBox.Tag = new JsonFormTag(
                "id_suffix",
                "You need to set this to a unique value if multiple recipes produce the same item.",
                false);
            skill1ComboBox.Tag = new JsonFormTag(
                "skill_used",
                "The main skill used in crafting this recipe.",
                false);
            ((JsonFormTag)skill1ComboBox.Tag).dataSource = JsonFormTag.DataSourceType.SKILLS;
            diffNumeric.Tag = new JsonFormTag(
                "difficulty",
                "The skill level required to craft this recipe.");
            timeNumeric.Tag = new JsonFormTag(
                "time",
                "The amount of time required to craft this recipe (in seconds?).");
            categoryComboBox.Tag = new JsonFormTag(
                "category",
                "The tab this recipe appears under in the crafting menu.");
            ((JsonFormTag)categoryComboBox.Tag).dataSource = JsonFormTag.DataSourceType.CRAFT_CATEGORIES;
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
            ((JsonFormTag)itemIdTextField.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;
            quantityNumeric.Tag = new JsonFormTag(
                null,
                "For components, the quantity used. For tools, the number of charges used (-1 for no charges).");

            bookIdTextBox.Tag = new JsonFormTag(
                null,
                "The string id of the book.");
            ((JsonFormTag)bookIdTextBox.Tag).dataSource = JsonFormTag.DataSourceType.BOOKS;
            bookReqLevelNumeric.Tag = new JsonFormTag(
                null,
                "The level required before this recipe can be learned from this book.");
            booksListBox.Tag = new JsonFormTag(
                "book_learn",
                "A list of books that this recipe might be learned from.",
                false);
            ListBoxTagData listBoxData = new ListBoxTagData();
            listBoxData.backingList = bookGroup;
            listBoxData.defaultValue = new object[] { "null", 0 };
            listBoxData.deleteButton = deleteBook;
            listBoxData.newButton = newBook;
            listBoxData.keyControl = bookIdTextBox;
            listBoxData.valueControl = bookReqLevelNumeric;
            ((JsonFormTag)booksListBox.Tag).listBoxData = listBoxData;

            reqSkillComboBox.Tag = new JsonFormTag(
                null,
                "The identifier of the skill required.");
            ((JsonFormTag)reqSkillComboBox.Tag).dataSource = JsonFormTag.DataSourceType.SKILLS;
            reqSkillLevelNumeric.Tag = new JsonFormTag(
                null,
                "The required level in the specified skill.");
            skillsListBox.Tag = new JsonFormTag(
                "requires_skills",
                "A list of skill levels required to craft this item.",
                false);
            listBoxData = new ListBoxTagData();
            listBoxData.backingList = requireSkills;
            listBoxData.defaultValue = new object[] { "null", 0 };
            listBoxData.deleteButton = deleteSkillButton;
            listBoxData.newButton = newSkillButton;
            listBoxData.keyControl = reqSkillComboBox;
            listBoxData.valueControl = reqSkillLevelNumeric;
            ((JsonFormTag)skillsListBox.Tag).listBoxData = listBoxData;

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);

            toolsListBox.DataSource = toolGroups;
            toolsListBox.DisplayMember = "Display";

            componentsListBox.DataSource = componentGroups;
            componentsListBox.DisplayMember = "Display";

            WinformsUtil.OnReset += Reset;
            WinformsUtil.OnLoadItem += LoadItem;
        }

        private void Reset()
        {
            WinformsUtil.Resetting++;
            toolGroups.Clear();
            componentGroups.Clear();
            itemsListBox.DataSource = null;
            WinformsUtil.Resetting--;
        }

        private void LoadItem(object item)
        {
            if (!Visible)
                return;

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
                //itemsListBox.SelectedIndex = -1;

                itemsListBox.DataSource = ((ComponentGroup)toolsListBox.SelectedItem).items;
                itemsListBox.DisplayMember = "Display";

                deleteToolButton.Enabled = true;
            }
            else
            {
                deleteToolButton.Enabled = false;
            }
        }

        private void componentsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (componentsListBox.SelectedItem != null)
            {
                toolsListBox.SelectedIndex = -1;
                //itemsListBox.SelectedIndex = -1;

                itemsListBox.DataSource = ((ComponentGroup)componentsListBox.SelectedItem).items;
                itemsListBox.DisplayMember = "Display";

                deleteComponentButton.Enabled = true;
            }
            else
            {
                deleteComponentButton.Enabled = false;
            }
        }

        private bool changing = false;

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinformsUtil.Resetting++;
            if (itemsListBox.SelectedItem != null)
            {
                itemIdTextField.Enabled = true;
                quantityNumeric.Enabled = true;
                itemIdTextField.Text = ((GroupedData)itemsListBox.SelectedItem).Id;
                quantityNumeric.Value = ((GroupedData)itemsListBox.SelectedItem).Value;

                deleteItemButton.Enabled = true;
            }
            else if (!changing)
            {
                ClearItem();
            }
            WinformsUtil.Resetting--;
        }

        private void ClearItem()
        {
            itemIdTextField.Enabled = false;
            quantityNumeric.Enabled = false;
            deleteItemButton.Enabled = false;
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
            if (toolsListBox.SelectedItem != null)
                SaveToolListToStorage();
            if (componentsListBox.SelectedItem != null)
                SaveComponentListToStorage();
        }

        private void SaveComponentListToStorage()
        {
            object[] cgroups = new object[componentGroups.Count];
            int c = 0;
            foreach (ComponentGroup cg in componentGroups)
            {
                object[] cgroup = new object[cg.items.Count];
                int d = 0;
                foreach (GroupedData cgi in cg.items)
                {
                    cgroup[d] = cgi.data;
                    d++;
                }
                cgroups[c] = cgroup;
                c++;
            }
            Storage.ItemApplyValue("components", cgroups, true);
        }

        private void SaveToolListToStorage()
        {
            object[] cgroups = new object[toolGroups.Count];
            int c = 0;
            foreach (ComponentGroup cg in toolGroups)
            {
                object[] cgroup = new object[cg.items.Count];
                int d = 0;
                foreach (GroupedData cgi in cg.items)
                {
                    cgroup[d] = cgi.data;
                    d++;
                }
                cgroups[c] = cgroup;
                c++;
            }
            Storage.ItemApplyValue("tools", cgroups, true);
        }

        private void itemIdTextField_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)itemsListBox.SelectedItem).Id = itemIdTextField.Text;
            changing = false;
            SaveItemlistToStorage();
        }

        private void quantityNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)itemsListBox.SelectedItem).Value = (int)quantityNumeric.Value;
            changing = false;
            SaveItemlistToStorage();
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            if (SelectedGroup != null)
            {
                SelectedGroup.AddNew();
                itemsListBox.SelectedIndex = itemsListBox.Items.Count - 1;
            }
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
            toolsListBox.SelectedIndex = toolsListBox.Items.Count - 1;
            toolsListBox_SelectedIndexChanged(null, null);
            SaveToolListToStorage();
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
                SaveToolListToStorage();
            }
        }

        private void newComponentButton_Click(object sender, EventArgs e)
        {
            componentGroups.Add(new ComponentGroup());
            componentsListBox.SelectedIndex = componentsListBox.Items.Count - 1;
            componentsListBox_SelectedIndexChanged(null, null);
            SaveComponentListToStorage();
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
                SaveComponentListToStorage();
            }
        }
    }
}
