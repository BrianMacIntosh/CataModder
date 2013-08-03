using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;

namespace CataclysmModder
{
    class ItemDataWrapper
    {
        public Dictionary<string, object> data;
        public string Display
        {
            get
            {
                if (data.ContainsKey("id"))
                    return (string)data["id"];
                else if (data.ContainsKey("ident"))
                    return (string)data["ident"];
                else if (data.ContainsKey("name"))
                    return (string)data["name"];
                else
                    return "item";
            }
        }

        public ItemDataWrapper(ItemDataWrapper copy)
        {
            data = new Dictionary<string, object>(copy.data);
            int lastitem = 0;
            foreach (ItemDataWrapper i in Storage.OpenItems)
                if (i.Display.StartsWith(copy.Display))
                {
                    try
                    {
                        lastitem = Math.Max(lastitem, Int32.Parse(i.Display.Substring(copy.Display.Length)));
                    }
                    catch (FormatException)
                    {

                    }
                }
            data["id"] = copy.Display + (lastitem + 1);
        }

        public ItemDataWrapper(Dictionary<string, object> data)
        {
            this.data = data;
        }

        public ItemDataWrapper()
        {
            data = new Dictionary<string, object>();
            int lastitem = 0;
            foreach (ItemDataWrapper i in Storage.OpenItems)
                if (i.Display.StartsWith("newitem"))
                {
                    try
                    {
                        lastitem = Math.Max(lastitem, Int32.Parse(i.Display.Substring(7)));
                    }
                    catch (FormatException)
                    {

                    }
                }
            data["id"] = "newitem" + (lastitem + 1);
        }
    }

    static class Storage
    {
        private static bool unsavedChanges = false;
        public static bool UnsavedChanges { get { return unsavedChanges; } }
        
        private static string workspacePath = "";
        private static string currentFileName = "";
        public static string CurrentFileName { get { return currentFileName; } }
        private static int currentItemIndex = -1;

        public static bool CurrentFileIsItems
        {
            get
            {
                string[] bits = currentFileName.Split('\\');
                foreach (string s in bits)
                    if (s.Equals("items", StringComparison.InvariantCultureIgnoreCase))
                        return true;
                return false;
            }
        }

        /// <summary>
        /// List of files found in the directory.
        /// </summary>
        public static string[] OpenFiles { get { return openFiles; } }
        private static string[] openFiles;

        /// <summary>
        /// Items in currently loaded file, slightly parsed
        /// </summary>
        public static BindingList<ItemDataWrapper> OpenItems { get { return openItems; } }
        private static BindingList<ItemDataWrapper> openItems;

        /// <summary>
        /// For items, JSON data on the selected item.
        /// </summary>
        public static Dictionary<string, object> CurrentItemData
        {
            get { return currentItemData; }
            set { currentItemData = value; }
        }
        public static Dictionary<string, object> currentItemData;

        public static bool FilesLoaded { get { return !string.IsNullOrEmpty(workspacePath); } }
        public static bool ItemsLoaded { get { return !string.IsNullOrEmpty(currentFileName); } }


        static Storage()
        {

        }

        public static void FileChanged()
        {
            unsavedChanges = true;
        }

        /// <summary>
        /// Load game files from the specified path.
        /// </summary>
        public static void LoadFiles(string path)
        {
            workspacePath = path;

            //Load all JSON files in directory and subs
            openFiles = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
            for (int c = 0; c < openFiles.Length; c++)
                openFiles[c] = openFiles[c].Substring(workspacePath.Length + 1);
        }

        /// <summary>
        /// Load up items from a specified game file.
        /// </summary>
        public static void LoadFile(string path)
        {
            //Load up contents of the file
            currentFileName = path;
            object json = LoadJson(path);
            
            //Parse different formats
            openItems = new BindingList<ItemDataWrapper>();
            string ffilename = Path.GetFileName(currentFileName);
            if (CurrentFileIsItems
                || ffilename.Equals("bionics.json")
                || ffilename.Equals("item_groups.json")
                || ffilename.Equals("materials.json")
                || ffilename.Equals("monstergroups.json")
                || ffilename.Equals("names.json")
                || ffilename.Equals("professions.json"))
            {
                foreach (Dictionary<string, object> item in (object[])json)
                    openItems.Add(new ItemDataWrapper(item));
            }
        }

        public static object LoadJson(string file)
        {
            StreamReader reader = new StreamReader(new FileStream(Path.Combine(workspacePath, file), FileMode.Open));
            string json = reader.ReadToEnd();
            try
            {
                return new JavaScriptSerializer().DeserializeObject(json);
            }
            catch (ArgumentException)
            {
                //TODO: error message
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public static void SaveJson(string file, object obj)
        {
            StreamWriter write = new StreamWriter(new FileStream(Path.Combine(workspacePath, file), FileMode.Create));
            try
            {
                string json = new JavaScriptSerializer().Serialize(obj);
                write.Write(SpaceJson(json));
                unsavedChanges = false;
            }
            catch (ArgumentException)
            {
                //TODO: error message
            }
            finally
            {
                write.Close();
            }
        }

        public static string SpaceJson(string json)
        {
            StringBuilder newjson = new StringBuilder();
            string nlindent = "\n";
            bool quoteOpen = false;
            bool escape = false;
            for (int c = 0; c < json.Length; c++)
            {
                if (!escape && json[c] == '"') quoteOpen = !quoteOpen;
                if (!escape && json[c] == '\\') escape = true;
                else escape = false;

                if (!quoteOpen && (json[c] == ']' || json[c] == '}'))
                {
                    nlindent = nlindent.Substring(0, nlindent.Length-1);
                    newjson.Append(nlindent + json[c]);
                }
                else if (!quoteOpen && (json[c] == '[' || json[c] == '{'))
                {
                    nlindent += '\t';
                    newjson.Append(json[c] + nlindent);
                }
                else if (!quoteOpen && json[c] == ',')
                {
                    newjson.Append(json[c] + nlindent);
                }
                else
                {
                    newjson.Append(json[c]);
                }
            }
            return newjson.ToString();
        }

        /// <summary>
        /// Load up one item from current file's JSON.
        /// </summary>
        /// <param name="id"></param>
        public static void LoadItem(int index)
        {
            if (index < 0) return;

            currentItemIndex = index;
            if (CurrentFileIsItems)
            {
                currentItemData = openItems[index].data;
                WinformsUtil.ControlsLoadItem(Form1.Instance.GenericItemControl.Controls[0], currentItemData);
            }
            else if (Path.GetFileName(CurrentFileName).Equals("item_groups.json"))
            {
                currentItemData = openItems[index].data;
                WinformsUtil.ControlsLoadItem(Form1.Instance.ItemGroupControl.Controls[0], currentItemData);
            }
        }

        public static void SetCurrentItem(Dictionary<string, object> item)
        {
            currentItemData = item;
            openItems[currentItemIndex].data = currentItemData;
        }

        public static void SaveCurrentFile()
        {
            string ffilename = Path.GetFileName(currentFileName);
            if (CurrentFileIsItems
                || ffilename.Equals("bionics.json")
                || ffilename.Equals("item_groups.json")
                || ffilename.Equals("materials.json")
                || ffilename.Equals("monstergroups.json")
                || ffilename.Equals("names.json")
                || ffilename.Equals("professions.json"))
            {
                object[] serialData = new object[openItems.Count];
                int c = 0;
                foreach (ItemDataWrapper v in openItems)
                {
                    serialData[c] = v.data;
                    c++;
                }
                SaveJson(currentFileName, serialData);
            }
            else
            {
                MessageBox.Show("Serializing this file is not supported.", "Error", MessageBoxButtons.OK);
                return;
            }
        }

        /// <summary>
        /// For items, apply a new value to the current item.
        /// </summary>
        public static void ItemApplyValue(string key, object value, bool mandatory)
        {
            if (!mandatory && value.Equals(""))
            {
                if (currentItemData.ContainsKey(key))
                    currentItemData.Remove(key);
            }
            else if (!currentItemData.ContainsKey(key) || value != currentItemData[key])
            {
                currentItemData[key] = value;
                unsavedChanges = true;
            }
        }

        public static void LoadMaterials(ListBox box)
        {
            box.Items.Clear();
            foreach (string s in openFiles)
            {
                if (Path.GetFileName(s).Equals("materials.json"))
                {
                    foreach (Dictionary<string, object> item in (object[])LoadJson(s))
                    {
                        box.Items.Add(item["ident"]);
                    }
                    return;
                }
            }
        }

        public static void LoadSkills(ComboBox box)
        {
            box.Items.Clear();
            box.Items.Add("none");
            foreach (string s in openFiles)
            {
                if (Path.GetFileName(s).Equals("skills.json"))
                {
                    foreach (object[] item in (object[])LoadJson(s))
                    {
                        box.Items.Add(item[0]);
                    }
                    return;
                }
            }
        }

        public static void LoadGunSkills(ComboBox box)
        {
            box.Items.Clear();
            box.Items.Add("none");
            foreach (string s in openFiles)
            {
                if (Path.GetFileName(s).Equals("skills.json"))
                {
                    foreach (object[] item in (object[])LoadJson(s))
                    {
                        foreach (string f in (object[])item[3])
                            if (f.Equals("gun_type"))
                            {
                                box.Items.Add(item[0]);
                                break;
                            }
                    }
                    return;
                }
            }
        }
    }
}
