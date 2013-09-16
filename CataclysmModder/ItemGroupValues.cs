using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ItemGroupValues : UserControl
    {
        private BindingList<GroupedData> items = new BindingList<GroupedData>();

        public ItemGroupValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string identifier for this item group,");
            itemidTextBox.Tag = new JsonFormTag(
                null,
                "The id of the item to spawn.");
            ((JsonFormTag)itemidTextBox.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;
            freqNumeric.Tag = new JsonFormTag(
                null,
                "The relative frequency of this item to spawn.");
            itemsListBox.Tag = new JsonFormTag(
                "items",
                "The list of items in this group and their relative frequencies.");
            ((JsonFormTag)itemsListBox.Tag).backingList = items;

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            items.Add(new GroupedData(new object[] { "null", 0 }));
            itemsListBox.SelectedIndex = itemsListBox.Items.Count - 1;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex >= 0)
                items.Remove((GroupedData)itemsListBox.SelectedItem);
        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinformsUtil.Resetting++;
            if (itemsListBox.SelectedItem != null)
            {
                //Fill fields
                itemidTextBox.Text = ((GroupedData)itemsListBox.SelectedItem).Id;
                itemidTextBox.Enabled = true;
                freqNumeric.Value = ((GroupedData)itemsListBox.SelectedItem).Value;
                freqNumeric.Enabled = true;

                deleteButton.Enabled = true;
            }
            else if (!changing)
            {
                itemidTextBox.Text = "";
                itemidTextBox.Enabled = false;
                freqNumeric.Value = 0;
                freqNumeric.Enabled = false;

                deleteButton.Enabled = false;
            }
            WinformsUtil.Resetting--;
        }

        private bool changing = false;

        private void itemidTextBox_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)itemsListBox.SelectedItem).Id = itemidTextBox.Text;
            changing = false;
        }

        private void freqNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            changing = true;
            ((GroupedData)itemsListBox.SelectedItem).Value = (int)freqNumeric.Value;
            changing = false;
        }
    }
}
