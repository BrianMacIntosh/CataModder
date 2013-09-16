using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CataclysmModder
{
    /// <summary>
    /// Set the Tag property on a control to one of these to control some automatic behavior.
    /// </summary>
    class JsonFormTag
    {
        public class HelpItem
        {
            public string item;
            public string help;

            public string Display { get { return item; } }

            public HelpItem(string item, string help)
            {
                this.item = item;
                this.help = help;
            }

            public override bool Equals(object obj)
            {
                if (obj is string)
                    return ((string)obj).ToLower().Equals(item.ToLower());
                else if (obj is HelpItem)
                    return obj == this;
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return item.ToLower().GetHashCode();
            }

            public override string ToString()
            {
                return Display;
            }
        }

        public enum DataSourceType
        {
            NONE,
            MATERIALS,
            SKILLS,
            GUN_SKILLS,
            CRAFT_CATEGORIES,
            ADDICTION_TYPES,
            ITEMS,
            BOOKS
        }

        /// <summary>
        /// The json key this control reads and writes.
        /// </summary>
        public string key;

        /// <summary>
        /// Help text to display when mousing over this control.
        /// </summary>
        public string help;

        /// <summary>
        /// The default value of this control.
        /// </summary>
        public object def;

        public bool mandatory = true;

        /// <summary>
        /// If set, this control has a list of values loaded from the specified source.
        /// </summary>
        public DataSourceType dataSource = DataSourceType.NONE;

        /// <summary>
        /// For listbox controls, the backing list of data items.
        /// </summary>
        public BindingList<GroupedData> backingList = null;


        public JsonFormTag(string key, string help)
            : this(key, help, true)
        {
            
        }

        public JsonFormTag(string key, string help, bool mandatory)
            : this(key, help, mandatory, null)
        {
            
        }

        public JsonFormTag(string key, string help, bool mandatory, object def)
        {
            this.key = key;
            this.help = help;
            this.mandatory = mandatory;
            this.def = def;
        }
    }


    /// <summary>
    /// This class sets up and manages the event-driven craziness that is the JSON-Winforms interface.
    /// </summary>
    static class WinformsUtil
    {
        /// <summary>
        /// Indicates that we should ignore data requests because a
        /// control is being reset.
        /// </summary>
        public static int Resetting = 0;

        public delegate void LoadItem(object item);
        public static LoadItem OnLoadItem;

        public delegate void Reset();
        public static Reset OnReset;

        private static List<Control> dataSourcedControls = new List<Control>();


        public static void ResetCheckedListBox(CheckedListBox box)
        {
            for (int c = 0; c < box.Items.Count; c++)
                box.SetItemChecked(c, false);
        }

        public static void SetString(Dictionary<string, object> itemValues, string key, Control field,
            string id, bool mandatory)
        {
            if (itemValues.ContainsKey(key))
            {
                try
                {
                    field.Text = (string)itemValues[key];
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Expected 'string' for key '" + key + "' but got '" + itemValues[key].GetType().ToString() + "'",
                        "Data Error", MessageBoxButtons.OK);
                }
            }
            else if (mandatory)
            {
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
            }
        }

        public static void SetInt(Dictionary<string, object> itemValues, string key, NumericUpDown field,
            string id, bool mandatory)
        {
            if (itemValues.ContainsKey(key))
            {
                try
                {
                    int val = (int)itemValues[key];
                    if (val > field.Maximum)
                    {
                        IssueTracker.PostIssue("Value '" + val + "' for key '" + key + "' exceeds the maximum allowed.",
                            IssueTracker.IssueLevel.WARNING);
                        val = (int)field.Maximum;
                    }
                    else if (val < field.Minimum)
                    {
                        IssueTracker.PostIssue("Value '" + val + "' for key '" + key + "' is below the minimum allowed.",
                            IssueTracker.IssueLevel.WARNING);
                        val = (int)field.Minimum;
                    }
                    field.Value = val;
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Expected 'int' for key '" + key + "' but got '" + itemValues[key].GetType().ToString() + "'",
                        "Data Error", MessageBoxButtons.OK);
                }
            }
            else if (mandatory)
            {
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
            }
        }

        public static void SetChecks(Dictionary<string, object> itemValues, string key, CheckedListBox field,
            string id, bool mandatory, bool material = false)
        {
            if (itemValues.ContainsKey(key))
            {
                if (itemValues[key] is object[])
                {
                    object[] tags = (object[])itemValues[key];
                    if (material && tags.Length > 2)
                        IssueTracker.PostIssue(
                            "Item '" + id + "': 'material' has too many items (expected 2).",
                            IssueTracker.IssueLevel.WARNING);
                    foreach (string str in tags)
                        SetCheck(id, str, field);
                }
                else
                {
                    try
                    {
                        SetCheck(id, (string)itemValues[key], field);
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Expected 'array' or 'string' for key '" + key + "' but got '" + itemValues[key].GetType().ToString() + "'",
                            "Data Error", MessageBoxButtons.OK);
                    }
                }
            }
            else if (mandatory)
            {
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
            }
        }

        public static void SetCheck(string id, string item, CheckedListBox field)
        {
            if (field.Items.Contains(item))
            {
                field.SetItemChecked(field.Items.IndexOf(item), true);
            }
            else
            {
                IssueTracker.PostIssue(
                    "Item '" + id + "': tag \"" + item + "\" doesn't exist. Will create...",
                    IssueTracker.IssueLevel.WARNING);

                //Create tag/mat
                //TODO:
            }
        }

        public static void SetCheckBox(Dictionary<string, object> itemValues, string key, CheckBox field,
            string id, bool mandatory, bool material = false)
        {
            if (itemValues.ContainsKey(key))
            {
                try
                {
                    field.Checked = (bool)itemValues[key];
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Expected 'bool' for key '" + key + "' but got '" + itemValues[key].GetType().ToString() + "'",
                        "Data Error", MessageBoxButtons.OK);
                }
            }
            else if (mandatory)
            {
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
            }
        }

        public static void SetList(Dictionary<string, object> itemValues, string key, ListBox box, BindingList<GroupedData> field,
            string id, bool mandatory)
        {
            if (itemValues.ContainsKey(key))
            {
                try
                {
                    foreach (object obj in (object[])itemValues[key])
                        field.Add(new GroupedData(obj));

                    if (box.Items.Count > 0)
                        box.SelectedIndex = 0; //TODO: THIS MIGHT NOT CALLBACK
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Expected 'object[]' for key '" + key + "' but got '" + itemValues[key].GetType().ToString() + "'",
                        "Data Error", MessageBoxButtons.OK);
                }
            }
            else if (mandatory)
            {
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
            }
        }

        /// <summary>
        /// Final call, sends value application to storage backend
        /// </summary>
        public static void ApplyValue(string key, object value, bool mandatory)
        {
            if (Resetting > 0) return;

            Storage.ItemApplyValue(key, value, mandatory);
        }

        public static void ApplyTags(string key, CheckedListBox box, ItemCheckEventArgs e)
        {
            if (Resetting > 0) return;
            
            object[] vals = new object[box.CheckedItems.Count + (e.NewValue == CheckState.Checked ? 1 : -1)];
            int d = 0;
            for (int c = 0; c < box.Items.Count; c++)
            {
                if (box.GetItemChecked(c))
                {
                    if (c != e.Index || e.NewValue != CheckState.Unchecked)
                    {
                        vals[d] = box.Items[c].ToString();
                        d++;
                    }
                }
            }
            if (e.NewValue == CheckState.Checked)
                vals[d] = box.Items[e.Index].ToString();

            if (key.Equals("material") && vals.Length == 1)
                ApplyValue(key, vals[0], ((JsonFormTag)box.Tag).mandatory);
            else
                ApplyValue(key, vals, ((JsonFormTag)box.Tag).mandatory);
        }

        public static void ApplyList(string key, BindingList<GroupedData> list, bool mandatory)
        {
            if (Resetting > 0) return;

            object[] iobj = new object[list.Count];
            int c = 0;
            foreach (GroupedData group in list)
            {
                iobj[c] = group.data;
                c++;
            }
            ApplyValue(key, iobj, mandatory);
        }

        public static void NumericValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            if (!string.IsNullOrEmpty(((JsonFormTag)num.Tag).key))
                ApplyValue(((JsonFormTag)num.Tag).key, (int)num.Value, ((JsonFormTag)num.Tag).mandatory);
        }

        public static void TextValueChanged(object sender, EventArgs e)
        {
            Control num = (Control)sender;
            if (!string.IsNullOrEmpty(((JsonFormTag)num.Tag).key))
                ApplyValue(((JsonFormTag)num.Tag).key, num.Text, ((JsonFormTag)num.Tag).mandatory);
        }

        public static void ChecksValueChanged(object sender, EventArgs e)
        {
            CheckedListBox num = (CheckedListBox)sender;
            if (!string.IsNullOrEmpty(((JsonFormTag)num.Tag).key))
                ApplyTags(((JsonFormTag)num.Tag).key, num, (ItemCheckEventArgs)e);
        }

        public static void CheckValueChanged(object sender, EventArgs e)
        {
            CheckBox num = (CheckBox)sender;
            if (!string.IsNullOrEmpty(((JsonFormTag)num.Tag).key))
                ApplyValue(((JsonFormTag)num.Tag).key, num.Checked, ((JsonFormTag)num.Tag).mandatory);
        }

        public static void ListChanged(object sender, EventArgs e)
        {
            BindingList<GroupedData> list = (BindingList<GroupedData>)sender;
            JsonFormTag tag = listTags[list];
            if (!string.IsNullOrEmpty(tag.key))
                ApplyList(tag.key, list, tag.mandatory);
        }

        public static void DisplayHelp(object sender, EventArgs e)
        {
            Form1.Instance.SetHelpText(((JsonFormTag)((Control)sender).Tag).help);
        }

        /// <summary>
        /// Remembers tags for BindingList controls, because they don't pass the control through the event.
        /// </summary>
        private static Dictionary<BindingList<GroupedData>, JsonFormTag> listTags =
            new Dictionary<BindingList<GroupedData>, JsonFormTag>();

        public static void ControlsAttachHooks(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag is JsonFormTag)
                {
                    JsonFormTag tag = (JsonFormTag)c.Tag;

                    c.Enter += DisplayHelp;

                    //Handle data source hooks
                    switch (tag.dataSource)
                    {
                        case JsonFormTag.DataSourceType.ITEMS:
                            if (!(c is TextBox))
                                throw new InvalidCastException("Item Data Source is only allowed on TextBox controls.");
                            TextBox tb1 = (TextBox)c;
                            tb1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            tb1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            tb1.AutoCompleteCustomSource = Storage.AutocompleteItemSource;
                            break;
                        case JsonFormTag.DataSourceType.BOOKS:
                            if (!(c is TextBox))
                                throw new InvalidCastException("Book Data Source is only allowed on TextBox controls.");
                            TextBox tb2 = (TextBox)c;
                            tb2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            tb2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            tb2.AutoCompleteCustomSource = Storage.AutocompleteBookSource;
                            break;
                        case JsonFormTag.DataSourceType.NONE:

                            break;
                        default:
                            dataSourcedControls.Add(c);
                            break;
                    }
                    
                    if (c is ComboBox)
                    {
                        ComboBox c1 = (ComboBox)c;
                        c1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        c1.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }

                    if (c is NumericUpDown)
                        ((NumericUpDown)c).ValueChanged += NumericValueChanged;
                    else if (c is CheckedListBox)
                        ((CheckedListBox)c).ItemCheck += ChecksValueChanged;
                    else if (c is CheckBox)
                        ((CheckBox)c).CheckedChanged += CheckValueChanged;
                    else if (c is ListBox && tag.backingList != null)
                    {
                        ((ListBox)c).DataSource = tag.backingList;
                        ((ListBox)c).DisplayMember = "Display";
                        tag.backingList.ListChanged += ListChanged;
                        listTags.Add(tag.backingList, tag);
                    }
                    else
                        c.TextChanged += TextValueChanged;
                }
                if (c.Controls.Count > 0)
                {
                    ControlsAttachHooks(c);
                }
            }
        }

        /// <summary>
        /// Set up the default...defaults, for controls.
        /// </summary>
        public static void TagsSetDefaults(Control controls)
        {
            foreach (Control c in controls.Controls)
            {
                if (c.Tag is JsonFormTag)
                {
                    JsonFormTag tag = (JsonFormTag)c.Tag;

                    if (tag.def == null)
                    {
                        if (c is NumericUpDown)
                            tag.def = 0;
                        else if (c is CheckedListBox)
                            tag.def = new string[0];
                        else if (c is CheckBox)
                            tag.def = false;
                        else
                            tag.def = "";
                    }
                }
                if (c.Controls.Count > 0)
                {
                    TagsSetDefaults(c);
                }
            }
        }

        /// <summary>
        /// Tells combo and list boxes pulling from data sources to reload them.
        /// </summary>
        public static void RefreshDataSources()
        {
            foreach (Control c in dataSourcedControls)
            {
                //Clear old data
                if (c is ListBox)
                    ((ListBox)c).Items.Clear();
                else if (c is ComboBox)
                    ((ComboBox)c).Items.Clear();
                else
                    throw new InvalidCastException("Data Sources that load lists are only permitted " +
                        "on ListBox and ComboBox controls");

                //Load new data
                string[] dataSource = null;
                switch (((JsonFormTag)c.Tag).dataSource)
                {
                    case JsonFormTag.DataSourceType.ADDICTION_TYPES:
                        dataSource = Storage.GetAddictions();
                        break;
                    case JsonFormTag.DataSourceType.CRAFT_CATEGORIES:
                        dataSource = Storage.GetCraftCategories();
                        break;
                    case JsonFormTag.DataSourceType.GUN_SKILLS:
                        dataSource = Storage.GetGunSkills();
                        break;
                    case JsonFormTag.DataSourceType.MATERIALS:
                        dataSource = Storage.GetMaterialNames();
                        break;
                    case JsonFormTag.DataSourceType.SKILLS:
                        dataSource = Storage.GetSkills();
                        break;
                }

                if (dataSource == null || dataSource.Length <= 0)
                    continue;

                //Populate new data
                if (c is ListBox)
                {
                    ListBox c1 = (ListBox)c;
                    foreach (string s in dataSource)
                        c1.Items.Add(s);
                }
                else if (c is ComboBox)
                {
                    ComboBox c1 = (ComboBox)c;
                    foreach (string s in dataSource)
                        c1.Items.Add(s);
                }
            }
        }

        /// <summary>
        /// Load data from the specified item into tagged controls in the specified control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="item"></param>
        public static void ControlsLoadItem(Control control, object item)
        {
            Resetting++;

            ControlSetValues(control, item);

            if (OnLoadItem != null)
                OnLoadItem(item);

            Resetting--;
        }

        private static void ControlSetValues(Control control, object item)
        {
            ControlsResetValues(control);

            Dictionary<string, object> itemValues = (Dictionary<string, object>)item;

            //TODO: displaymember?
            string id = "-null-";
            if (itemValues.ContainsKey("id"))
                id = (string)itemValues["id"];

            foreach (Control c in control.Controls)
            {
                if (c.Tag is JsonFormTag)
                {
                    JsonFormTag tag = (JsonFormTag)c.Tag;
                    if (!string.IsNullOrEmpty(tag.key))
                    {
                        if (c is NumericUpDown)
                            SetInt(itemValues, tag.key, (NumericUpDown)c, id, tag.mandatory);
                        else if (c is CheckedListBox)
                            SetChecks(itemValues, tag.key, (CheckedListBox)c, id, tag.mandatory);
                        else if (c is CheckBox)
                            SetCheckBox(itemValues, tag.key, (CheckBox)c, id, tag.mandatory);
                        else if (c is ListBox && tag.backingList != null)
                            SetList(itemValues, tag.key, (ListBox)c, tag.backingList, id, tag.mandatory);
                        else
                            SetString(itemValues, tag.key, c, id, tag.mandatory);
                    }
                }
                if (c.Controls.Count > 0)
                {
                    ControlSetValues(c, item);
                }
            }
        }

        /// <summary>
        /// Reset the values of tagged controls in the specified control.
        /// </summary>
        /// <param name="control"></param>
        public static void ControlsResetValues(Control control)
        {
            Resetting++;
            if (OnReset != null) OnReset();

            ControlResetValues(control);

            Resetting--;
        }

        private static void ControlResetValues(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag is JsonFormTag)
                {
                    JsonFormTag tag = (JsonFormTag)c.Tag;
                    if (c is NumericUpDown)
                    {
                        if (tag.def != null)
                            ((NumericUpDown)c).Value = (int)tag.def;
                        else
                            ((NumericUpDown)c).Value = 0;
                    }
                    else if (c is CheckedListBox)
                    {
                        ResetCheckedListBox((CheckedListBox)c);
                    }
                    else if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = (bool)tag.def;
                    }
                    else if (tag.backingList != null)
                    {
                        tag.backingList.Clear();
                    }
                    else
                    {
                        if (tag.def != null)
                            c.Text = (string)tag.def;
                        else
                            c.Text = "";
                    }
                }
                if (c.Controls.Count > 0)
                {
                    ControlResetValues(c);
                }
            }
        }
    }
}
