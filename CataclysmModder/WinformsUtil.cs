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
            //Preset values (these can be modified if files have new ones)
            ADDICTION_TYPES,
            AMMO_EFFECTS,
            BODY_PARTS,
            TECHNIQUES, //TODO: actually loaded in dev version
            FLAGS,
            VEHICLEPART_FLAGS,
            FUEL,
            PRESET_MOD_COUNT,

            //Non
            COLOR,
            PRESET_COUNT,

            //Loaded values (loaded from the game data)
            MATERIALS,
            SKILLS,
            GUN_SKILLS,
            CRAFT_CATEGORIES,

            //Other values
            NONE,
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
        /// Additional information for basic listbox controls.
        /// </summary>
        public ListBoxTagData listBoxData = null;

        public ListBox ownerListBox;


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


    class ListBoxTagData
    {
        /// <summary>
        /// A list storing the actual data for the listbox.
        /// </summary>
        public BindingList<GroupedData> backingList = null;

        public Button newButton;
        public Button deleteButton;

        public Control keyControl;
        public NumericUpDown valueControl;

        public object defaultValue;
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
            if (!field.Items.Contains(item))
            {
                if (((JsonFormTag)field.Tag).dataSource > JsonFormTag.DataSourceType.PRESET_COUNT)
                {
                    IssueTracker.PostIssue(
                        "Item '" + id + "': tag \"" + item + "\" doesn't exist and cannot add.",
                        IssueTracker.IssueLevel.ERROR);
                    return;
                }

                IssueTracker.PostIssue(
                    "Item '" + id + "': tag \"" + item + "\" doesn't exist. Will add...",
                    IssueTracker.IssueLevel.WARNING);

                //Create tag/mat
                Storage.UpdateDataSource(((JsonFormTag)field.Tag).dataSource, item);
            }

            field.SetItemChecked(field.Items.IndexOf(item), true);
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
                if (itemValues[key] is object[])
                {
                    object[] objarr = (object[])itemValues[key];
                    if (objarr.Length > 0)
                    {
                        object subobject = objarr[0];
                        if (((JsonFormTag)box.Tag).listBoxData.defaultValue.GetType().IsAssignableFrom(subobject.GetType()))
                        {
                            foreach (object obj in objarr)
                                field.Add(new GroupedData(obj));
                        }
                        else
                        {
                            field.Add(new GroupedData(itemValues[key]));
                        }
                    }
                }
                else
                {
                    field.Add(new GroupedData(itemValues[key]));
                }

                if (box.Items.Count > 0)
                    box.SelectedIndex = 0; //TODO: THIS MIGHT NOT CALLBACK
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

        private static void ControlsFillTags(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag is JsonFormTag)
                {
                    //Fill in list component owner fields
                    ListBoxTagData listBoxData = ((JsonFormTag)c.Tag).listBoxData;
                    if (listBoxData != null)
                    {
                        //TODO: better error checking
                        listBoxData.newButton.Tag = new JsonFormTag(null, null);
                        ((JsonFormTag)listBoxData.newButton.Tag).ownerListBox = (ListBox)c;

                        listBoxData.deleteButton.Tag = new JsonFormTag(null, null);
                        ((JsonFormTag)listBoxData.deleteButton.Tag).ownerListBox = (ListBox)c;

                        ((JsonFormTag)listBoxData.keyControl.Tag).ownerListBox = (ListBox)c;

                        if (listBoxData.valueControl != null)
                            ((JsonFormTag)listBoxData.valueControl.Tag).ownerListBox = (ListBox)c;
                    }
                }
                if (c.Controls.Count > 0)
                {
                    ControlsFillTags(c);
                }
            }
        }

        public static void ControlsAttachHooks(Control control)
        {
            ControlsFillTags(control);

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
                    else if (c is ListBox && tag.listBoxData != null)
                    {
                        //Set up data
                        ((ListBox)c).DataSource = tag.listBoxData.backingList;
                        ((ListBox)c).DisplayMember = "Display";
                        listTags.Add(tag.listBoxData.backingList, tag);

                        tag.listBoxData.backingList.ListChanged += ListChanged;

                        //Attach other hooks
                        ((ListBox)c).SelectedIndexChanged += ListSelectedIndexChanged;
                        tag.listBoxData.newButton.Click += ListBoxNewClicked;
                        tag.listBoxData.deleteButton.Click += ListBoxDeleteClicked;
                        tag.listBoxData.keyControl.TextChanged += ListBoxKeyChanged;
                        if (tag.listBoxData.valueControl != null)
                            tag.listBoxData.valueControl.ValueChanged += ListBoxValueChanged;

                        //Disable
                        tag.listBoxData.deleteButton.Enabled = false;
                        tag.listBoxData.keyControl.Enabled = false;
                        if (tag.listBoxData.valueControl != null)
                            tag.listBoxData.valueControl.Enabled = false;
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

        #region List Box Callbacks

        private static bool changingListValues = false;

        static void ListSelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            ListBoxTagData tagData = ((JsonFormTag)listBox.Tag).listBoxData;
            WinformsUtil.Resetting++;
            if (listBox.SelectedItem != null)
            {
                tagData.keyControl.Enabled = true;
                tagData.keyControl.Text = ((GroupedData)listBox.SelectedItem).Id;
                if (tagData.valueControl != null)
                {
                    tagData.valueControl.Enabled = true;
                    tagData.valueControl.Value = ((GroupedData)listBox.SelectedItem).Value;
                }

                tagData.deleteButton.Enabled = true;
            }
            else if (!changingListValues)
            {
                tagData.keyControl.Enabled = false;
                tagData.keyControl.Text = "";
                if (tagData.valueControl != null)
                {
                    tagData.valueControl.Enabled = false;
                    tagData.valueControl.Value = 0;
                }

                tagData.deleteButton.Enabled = false;
            }
            WinformsUtil.Resetting--;
        }

        static void ListBoxNewClicked(object sender, EventArgs e)
        {
            ListBox owner = ((JsonFormTag)((Control)sender).Tag).ownerListBox;
            ListBoxTagData listBoxData = ((JsonFormTag)owner.Tag).listBoxData;

            //Add a new entry with the default data
            if (listBoxData.defaultValue is object[])
            {
                object[] defArr = ((object[])listBoxData.defaultValue);
                object[] data = new object[defArr.Length];
                defArr.CopyTo(data, 0);
                listBoxData.backingList.Add(new GroupedData(data));
            }
            else if (listBoxData.defaultValue is Dictionary<string, object>)
            {
                Dictionary<string, object> defDict = ((Dictionary<string, object>)listBoxData.defaultValue);
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> kv in defDict)
                    data.Add(kv.Key, kv.Value);
                listBoxData.backingList.Add(new GroupedData(data));
            }
            else
            {
                listBoxData.backingList.Add(new GroupedData(listBoxData.defaultValue));
            }

            owner.SelectedIndex = owner.Items.Count - 1;
        }

        static void ListBoxDeleteClicked(object sender, EventArgs e)
        {
            ListBox owner = ((JsonFormTag)((Control)sender).Tag).ownerListBox;
            ListBoxTagData listBoxData = ((JsonFormTag)owner.Tag).listBoxData;
            if (owner.SelectedItem != null)
                listBoxData.backingList.Remove((GroupedData)owner.SelectedItem);
        }

        static void ListBoxKeyChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            ListBox owner = ((JsonFormTag)((Control)sender).Tag).ownerListBox;

            if (owner.SelectedItem == null) return;

            changingListValues = true;
            ((GroupedData)owner.SelectedItem).Id = ((Control)sender).Text;
            changingListValues = false;
        }

        static void ListBoxValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            ListBox owner = ((JsonFormTag)((Control)sender).Tag).ownerListBox;

            if (owner.SelectedItem == null) return;

            changingListValues = true;
            ((GroupedData)owner.SelectedItem).Value = (int)((NumericUpDown)sender).Value;
            changingListValues = false;
        }

        #endregion

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
                    throw new ArgumentException("Data Sources that load lists are only permitted " +
                        "on ListBox and ComboBox controls");

                //Load new data
                string[] dataSource = Storage.GetDataSource(((JsonFormTag)c.Tag).dataSource);

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
                        else if (c is ListBox && tag.listBoxData != null)
                            SetList(itemValues, tag.key, (ListBox)c, tag.listBoxData.backingList, id, tag.mandatory);
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
                    else if (tag.listBoxData != null)
                    {
                        tag.listBoxData.backingList.Clear();
                    }
                    else if (!(c is Button))
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

        /// <summary>
        /// Configure a label to display a colored in-game character.
        /// </summary>
        public static void SetupCharDisplayLabel(Label label, TextBox character, ComboBox color)
        {
            character.TextChanged += (s, e) => label.Text = ((TextBox)s).Text;
            color.TextChanged += (s, e) => label.ForeColor = CataColor.GetColorFgByName(((ComboBox)s).Text);
        }
    }
}
