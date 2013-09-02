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
    public partial class ItemGroupValues : UserControl
    {
        private BindingList<ItemGroupLine> items = new BindingList<ItemGroupLine>();

        public ItemGroupValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string identifier for this item group,");
            itemidTextBox.Tag = new JsonFormTag(
                null,
                "The id of the item to spawn.");
            ((JsonFormTag)itemidTextBox.Tag).isItemId = true;
            freqNumeric.Tag = new JsonFormTag(
                null,
                "The relative frequency of this item to spawn.");
            itemsListBox.Tag = new JsonFormTag(
                null,
                "The list of items in this group and their relative frequencies.");

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);

            itemsListBox.DataSource = items;
            itemsListBox.DisplayMember = "Display";

            WinformsUtil.OnReset += Reset;
            WinformsUtil.OnLoadItem += LoadItem;
        }

        private void Reset()
        {
            WinformsUtil.Resetting++;
            itemidTextBox.Text = "";
            freqNumeric.Value = 0;
            items.Clear();
            WinformsUtil.Resetting--;
        }

        private void LoadItem(object item)
        {
            //Wrap items into the list
            items.Clear();

            Dictionary<string, object> dict = (Dictionary<string, object>)item;
            if (dict.ContainsKey("items"))
                foreach (object[] data in (object[])dict["items"])
                    items.Add(new ItemGroupLine(data));

            if (items.Count > 0)
            {
                itemsListBox.SelectedIndex = 0;
                itemsListBox_SelectedIndexChanged(null, null);
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            items.Add(new ItemGroupLine());
            SaveItemlistToStorage();
            itemsListBox.SelectedIndex = itemsListBox.Items.Count - 1;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex >= 0)
            {
                items.Remove((ItemGroupLine)itemsListBox.SelectedItem);
                SaveItemlistToStorage();
            }
        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinformsUtil.Resetting++;
            if (itemsListBox.SelectedItem != null)
            {
                //Fill fields
                itemidTextBox.Text = ((ItemGroupLine)itemsListBox.SelectedItem).Id;
                itemidTextBox.Enabled = true;
                freqNumeric.Value = ((ItemGroupLine)itemsListBox.SelectedItem).Value;
                freqNumeric.Enabled = true;
            }
            else if (!changing)
            {
                itemidTextBox.Text = "";
                itemidTextBox.Enabled = false;
                freqNumeric.Value = 0;
                freqNumeric.Enabled = false;
            }
            WinformsUtil.Resetting--;
        }

        private bool changing = false;

        private void itemidTextBox_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((ItemGroupLine)itemsListBox.SelectedItem).Id = itemidTextBox.Text;
            changing = false;
            SaveItemlistToStorage();
        }

        private void freqNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((ItemGroupLine)itemsListBox.SelectedItem).Value = (int)freqNumeric.Value;
            changing = false;
            SaveItemlistToStorage();
        }

        private void SaveItemlistToStorage()
        {
            WinformsUtil.ApplyItemGroupLines("items", items, true);
        }
    }
}
