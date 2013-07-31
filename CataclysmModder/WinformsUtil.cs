using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CataclysmModder
{
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
        }

        public string key;
        public string help;
        public object def;
        public bool mandatory = true;
        public BindingList<HelpItem> listitems;

        public JsonFormTag(string key, string help)
        {
            this.key = key;
            this.help = help;
        }

        public JsonFormTag(string key, string help, bool mandatory)
        {
            this.key = key;
            this.help = help;
            this.mandatory = mandatory;
        }

        public JsonFormTag(string key, string help, bool mandatory, object def)
        {
            this.key = key;
            this.help = help;
            this.mandatory = mandatory;
            this.def = def;
        }
    }

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


        public static void ResetCheckedListBox(CheckedListBox box)
        {
            for (int c = 0; c < box.Items.Count; c++)
                box.SetItemChecked(c, false);
        }

        public static void SetString(Dictionary<string, object> itemValues, string key, Control field,
            string id, bool mandatory)
        {
            if (itemValues.ContainsKey(key))
                field.Text = (string)itemValues[key];
            else if (mandatory)
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
        }

        public static void SetInt(Dictionary<string, object> itemValues, string key, NumericUpDown field,
            string id, bool mandatory)
        {
            if (itemValues.ContainsKey(key))
                field.Value = (int)itemValues[key];
            else if (mandatory)
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
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
                    SetCheck(id, (string)itemValues[key], field);
                }
            }
            else if (mandatory)
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
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
                field.Checked = (bool)itemValues[key];
            else if (mandatory)
                IssueTracker.PostIssue(
                    "Item '" + id + "': missing mandatory value for '" + key + "'.",
                    IssueTracker.IssueLevel.ERROR);
        }

        /// <summary>
        /// Final call, sends value application to storage backend
        /// </summary>
        public static void ApplyValue(string key, object value)
        {
            if (Resetting > 0) return;

            Storage.ItemApplyValue(key, value);
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
                        vals[d] = box.Items[c];
                        d++;
                    }
                }
            }
            if (e.NewValue == CheckState.Checked)
                vals[d] = box.Items[e.Index];
            ApplyValue(key, vals);
        }

        public static void NumericValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            ApplyValue(((JsonFormTag)num.Tag).key, (int)num.Value);
        }

        public static void TextValueChanged(object sender, EventArgs e)
        {
            Control num = (Control)sender;
            ApplyValue(((JsonFormTag)num.Tag).key, num.Text);
        }

        public static void ChecksValueChanged(object sender, EventArgs e)
        {
            CheckedListBox num = (CheckedListBox)sender;
            ApplyTags(((JsonFormTag)num.Tag).key, num, (ItemCheckEventArgs)e);
        }

        public static void CheckValueChanged(object sender, EventArgs e)
        {
            CheckBox num = (CheckBox)sender;
            ApplyValue(((JsonFormTag)num.Tag).key, num.Checked);
        }

        public static void DisplayHelp(object sender, EventArgs e)
        {
            Form1.Instance.SetHelpText(((JsonFormTag)((Control)sender).Tag).help);
        }


        public static void ControlsAttachHooks(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag is JsonFormTag)
                {
                    c.Enter += DisplayHelp;

                    if (c is NumericUpDown)
                        ((NumericUpDown)c).ValueChanged += NumericValueChanged;
                    else if (c is CheckedListBox)
                        ((CheckedListBox)c).ItemCheck += ChecksValueChanged;
                    else if (c is CheckBox)
                        ((CheckBox)c).CheckedChanged += CheckValueChanged;
                    else
                        c.TextChanged += TextValueChanged;
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
            }
        }

        public static void ControlsLoadItem(Control control, object item)
        {
            ControlsResetValues(control);
            Resetting++;
            if (OnLoadItem != null) OnLoadItem(item);

            Dictionary<string, object> itemValues = (Dictionary<string, object>)item;

            string id = "-null-";
            if (itemValues.ContainsKey("id"))
                id = (string)itemValues["id"];

            foreach (Control c in control.Controls)
            {
                if (c.Tag is JsonFormTag)
                {
                    JsonFormTag tag = (JsonFormTag)c.Tag;
                    if (c is NumericUpDown)
                        SetInt(itemValues, tag.key, (NumericUpDown)c, id, tag.mandatory);
                    else if (c is CheckedListBox)
                        SetChecks(itemValues, tag.key, (CheckedListBox)c, id, tag.mandatory);
                    else if (c is CheckBox)
                        SetCheckBox(itemValues, tag.key, (CheckBox)c, id, tag.mandatory);
                    else
                        SetString(itemValues, tag.key, c, id, tag.mandatory);
                }
            }

            Resetting--;
        }

        public static void ControlsResetValues(Control control)
        {
            Resetting++;
            if (OnReset != null) OnReset();
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
                    else
                    {
                        if (tag.def != null)
                            c.Text = (string)tag.def;
                        else
                            c.Text = "";
                    }
                }
            }
            Resetting--;
        }
    }
}
