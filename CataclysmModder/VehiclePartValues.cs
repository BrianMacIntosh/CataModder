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
    public partial class VehiclePartValues : UserControl
    {
        public VehiclePartValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string identifier for this part.");
            nameTextBox.Tag = new JsonFormTag(
                "name",
                "The display name for this part in vehicle menus.");
            symbolTextBox.Tag = new JsonFormTag(
                "symbol",
                "The display symbol for this part on the map.");
            colorComboBox.Tag = new JsonFormTag(
                "color",
                "The display color for this part on the map.");
            ((JsonFormTag)colorComboBox.Tag).dataSource = JsonFormTag.DataSourceType.COLOR;
            brokeSymbolTextBox.Tag = new JsonFormTag(
                "broken_symbol",
                "The display symbol for the broken version of this part on the map.");
            brokeColorComboBox.Tag = new JsonFormTag(
                "broken_color",
                "The display color for the broken version of this part on the map.");
            ((JsonFormTag)brokeColorComboBox.Tag).dataSource = JsonFormTag.DataSourceType.COLOR;
            damModNumeric.Tag = new JsonFormTag(
                "damage_modifier",
                "A percentage applied to damage taken by this part. If 0, the part will not take damage.",
                false,
                0);
            itemNameTextBox.Tag = new JsonFormTag(
                "item",
                "The id of the item this part is built out of.");
            ((JsonFormTag)itemNameTextBox.Tag).dataSource = JsonFormTag.DataSourceType.ITEMS;
            vehicleFlagsListBox.Tag = new JsonFormTag(
                "flags",
                "A list of flags that apply certain properties to this part.");
            ((JsonFormTag)vehicleFlagsListBox.Tag).dataSource = JsonFormTag.DataSourceType.VEHICLEPART_FLAGS;
            fuelTypeComboBox.Tag = new JsonFormTag(
                "fuel_type",
                "The type of fuel this part uses, if it has the ENGINE flag.",
                false);
            ((JsonFormTag)fuelTypeComboBox.Tag).dataSource = JsonFormTag.DataSourceType.FUEL;

            par1Numeric.Tag = new JsonFormTag(
                "par1",
                "");
            powerNumeric.Tag = new JsonFormTag(
                "power",
                "");
            sizeNumeric.Tag = new JsonFormTag(
                "size",
                "");
            wheelWidthNumeric.Tag = new JsonFormTag(
                "wheel_width",
                "The width of this part if it has the WHEEL flag.");
            bonusNumeric.Tag = new JsonFormTag(
                "bonus",
                "");

            WinformsUtil.SetupCharDisplayLabel(charDisplayLabel, symbolTextBox, colorComboBox);
            WinformsUtil.SetupCharDisplayLabel(charDisplayLabelBroken, brokeSymbolTextBox, brokeColorComboBox);

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);

            WinformsUtil.OnLoadItem += LoadItem;
        }

        void LoadItem(object item)
        {
            //Check the proper par union box
            if (Storage.CurrentItemData.ContainsKey("par1"))
                par1RadioButton.Checked = true;
            else if (Storage.CurrentItemData.ContainsKey("power"))
                powerRadioButton.Checked = true;
            else if (Storage.CurrentItemData.ContainsKey("size"))
                sizeRadioButton.Checked = true;
            else if (Storage.CurrentItemData.ContainsKey("wheel_width"))
                wheelWidthRadioButton.Checked = true;
            else if (Storage.CurrentItemData.ContainsKey("bonus"))
                bonusRadioButton.Checked = true;
            else
                par1RadioButton.Checked = true;
        }

        private void partRadioChanged(object sender, EventArgs e)
        {
            par1Numeric.Enabled = par1RadioButton.Checked;
            sizeNumeric.Enabled = sizeRadioButton.Checked;
            powerNumeric.Enabled = powerRadioButton.Checked;
            wheelWidthNumeric.Enabled = wheelWidthRadioButton.Checked;
            bonusNumeric.Enabled = bonusRadioButton.Checked;
        }
    }
}
