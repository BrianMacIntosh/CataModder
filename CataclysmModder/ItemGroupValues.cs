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
        private class ItemGroupLine
        {
            public object[] data;

            public string Id
            {
                get { return (string)data[0]; }
                set { data[0] = value; }
            }
            public int Freq
            {
                get { return (int)data[1]; }
                set { data[1] = value; }
            }

            public string Display
            {
                get
                {
                    return Id + " (" + Freq + ")";
                }
            }

            public ItemGroupLine(object[] data)
            {
                this.data = data;
            }
        }

        private BindingList<ItemGroupLine> items = new BindingList<ItemGroupLine>();

        public ItemGroupValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string identifier for this item group,");

            WinformsUtil.ControlsAttachHooks(Controls[0]);

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
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            items.Add(new ItemGroupLine(new object[] { "null", 0 }));
            SaveItemlistToStorage();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex > 0)
            {
                items.Remove((ItemGroupLine)itemsListBox.SelectedItem);
                SaveItemlistToStorage();
            }
        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex < 0)
            {
                itemidTextBox.Text = "";
                itemidTextBox.Enabled = false;
                freqNumeric.Value = 0;
                freqNumeric.Enabled = false;
            }
            else
            {
                //Fill fields
                itemidTextBox.Text = ((ItemGroupLine)itemsListBox.SelectedItem).Id;
                itemidTextBox.Enabled = true;
                freqNumeric.Value = ((ItemGroupLine)itemsListBox.SelectedItem).Freq;
                freqNumeric.Enabled = true;
            }
        }

        private void itemidTextBox_TextChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            ((ItemGroupLine)itemsListBox.SelectedItem).Id = itemidTextBox.Text;
            SaveItemlistToStorage();

            //Force display to update
            items.Add(new ItemGroupLine(new object[] { "", 0 }));
            items.RemoveAt(items.Count - 1);
        }

        private void freqNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (WinformsUtil.Resetting > 0) return;

            ((ItemGroupLine)itemsListBox.SelectedItem).Freq = (int)freqNumeric.Value;
            SaveItemlistToStorage();

            //Force display to update
            items.Add(new ItemGroupLine(new object[] { "",0 }));
            items.RemoveAt(items.Count - 1);
        }

        private void SaveItemlistToStorage()
        {
            object[] iobj = new object[items.Count];
            int c = 0;
            foreach (ItemGroupLine ig in items)
            {
                iobj[c] = ig.data;
                c++;
            }
            Storage.ItemApplyValue("items", iobj);
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
