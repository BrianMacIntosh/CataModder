using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ExportItemsForm : Form
    {
        public ExportItemsForm()
        {
            InitializeComponent();

            //Load up listbox
            checkedListBox1.Items.Clear();
            foreach (ItemDataWrapper data in Storage.OpenItems)
            {
                checkedListBox1.Items.Add(data.Display);
            }

            int index = 0;
            foreach (ItemDataWrapper data in Storage.OpenItems)
            {
                checkedListBox1.SetItemChecked(index, data.Modified);
                index++;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.AddExtension = true;
            save.DefaultExt = "json";
            save.OverwritePrompt = true;
            save.Title = "Export Items";

            if (save.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
