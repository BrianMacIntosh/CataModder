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
            ListBoxTagData listBoxData = new ListBoxTagData();
            listBoxData.backingList = items;
            listBoxData.defaultValue = new object[] { "null", 0 };
            listBoxData.deleteButton = deleteButton;
            listBoxData.newButton = newButton;
            listBoxData.keyControl = itemidTextBox;
            listBoxData.valueControl = freqNumeric;
            ((JsonFormTag)itemsListBox.Tag).listBoxData = listBoxData;

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);
        }
    }
}
