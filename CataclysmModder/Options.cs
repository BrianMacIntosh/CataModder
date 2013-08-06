using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CataclysmModder
{
    public partial class Options : Form
    {
        public static bool DontFormatJson = false;
        public static bool IndentWithTabs = false;
        public static int IndentSpaces = 4;

        public Options()
        {
            InitializeComponent();

            LoadOptions();

            dontFormatJsonCheck.Checked = DontFormatJson;
            indentTabsCheck.Checked = IndentWithTabs;
            indentSpacesNumeric.Value = IndentSpaces;
        }

        private void dontFormatJsonCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dontFormatJsonCheck.Checked)
            {
                indentTabsCheck.Enabled = false;
                indentSpacesNumeric.Enabled = false;
            }
            else
            {
                indentTabsCheck.Enabled = true;
                if (!indentTabsCheck.Checked)
                    indentSpacesNumeric.Enabled = true;
            }
        }

        private void indentTabsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (indentTabsCheck.Checked)
            {
                indentSpacesNumeric.Enabled = false;
            }
            else
            {
                if (!dontFormatJsonCheck.Checked)
                    indentSpacesNumeric.Enabled = true;
            }
        }

        private void SaveOptions()
        {
            StreamWriter write = new StreamWriter(new FileStream("options.ini", FileMode.Create));
            write.WriteLine("dontFormatJson = " + DontFormatJson);
            write.WriteLine("indentWithTabs = " + IndentWithTabs);
            write.WriteLine("indentSpaces = " + IndentSpaces);
            write.Close();
        }

        private void LoadOptions()
        {
            if (!File.Exists("options.ini")) return;

            StreamReader read = new StreamReader(new FileStream("options.ini", FileMode.Open));
            while (!read.EndOfStream)
            {
                string line = read.ReadLine();
                string key = "";
                string value = "";
                int mode = 0;
                for (int c = 0; c < line.Length; c++)
                {
                    if (mode == 0)
                    {
                        if (line[c] == '=')
                            mode = 1;
                        else
                            key += line[c];
                    }
                    else if (mode == 1)
                    {
                        value += line[c];
                    }
                }

                try
                {
                    key = key.Trim().ToLower();
                    if (key.Equals("dontformatjson"))
                        DontFormatJson = bool.Parse(value.Trim());
                    else if (key.Equals("indentwithtabs"))
                        IndentWithTabs = bool.Parse(value.Trim());
                    else if (key.Equals("indentspaces"))
                        IndentSpaces = int.Parse(value.Trim());
                }
                catch (FormatException)
                {
                    //TODO: error
                }
            }
            read.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            DontFormatJson = dontFormatJsonCheck.Checked;
            IndentWithTabs = indentTabsCheck.Checked;
            IndentSpaces = (int)indentSpacesNumeric.Value;

            SaveOptions();

            this.Close();
        }
    }
}
