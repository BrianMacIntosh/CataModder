using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;

namespace CataclysmModder
{
    class ItemDataWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyKeyChanged(string key)
        {
            //TODO: entries box still doesn't update on name change
            foreach (string s in DisplayMembers)
                if (s.Equals(key))
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Display"));
                    return;
                }
        }

        public static string[] DisplayMembers = new string[] { "id", "ident", "name", "result" };
        
        public Dictionary<string, object> data;
        public string Display
        {
            get
            {
                foreach (string s in DisplayMembers)
                    if (data.ContainsKey(s))
                        return (string)data[s];
                return "[item]";
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

            //TODO: might need a different display member
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

            //TODO: might need a different display member
            data["id"] = "newitem" + (lastitem + 1);
        }
    }

    static class Storage
    {
        private static bool unsavedChanges = false;
        public static bool UnsavedChanges { get { return unsavedChanges; } }
        
        private static string workspacePath = "";
        private static int currentFileIndex = -1;
        public static string CurrentFileName
        {
            get
            {
                if (currentFileIndex >= 0)
                    return openFiles[currentFileIndex];
                else
                    return "";
            }
        }
        private static int currentItemIndex = -1;
        public static Dictionary<string, object> CurrentItemData
        {
            get
            {
                if (currentItemIndex >= 0)
                    return openItems[currentFileIndex][currentItemIndex].data;
                else
                    return null;
            }
            set
            {
                openItems[currentFileIndex][currentItemIndex].data = value;
            }
        }

        public static bool CurrentFileIsItems
        {
            get { return FileIsItems(CurrentFileName); }
        }

        public static bool FileIsItems(string name)
        {
            string[] bits = name.Split('\\');
            foreach (string s in bits)
                if (s.Equals("items", StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }

        /// <summary>
        /// List of files found in the directory.
        /// </summary>
        public static string[] OpenFiles { get { return openFiles; } }
        private static string[] openFiles;

        /// <summary>
        /// Items in currently loaded files, slightly parsed
        /// </summary>
        public static BindingList<ItemDataWrapper> OpenItems
        {
            get
            {
                if (currentFileIndex >= 0)
                    return openItems[currentFileIndex];
                else
                    return null;
            }
        }
        public static List<BindingList<ItemDataWrapper>> openItems = new List<BindingList<ItemDataWrapper>>();

        public static bool FilesLoaded { get { return !string.IsNullOrEmpty(workspacePath); } }
        public static bool ItemsLoaded { get { return currentFileIndex >= 0; } }

        public static object[] CraftCategories;

        public static AutoCompleteStringCollection AutocompleteItemSource = new AutoCompleteStringCollection();


        static Storage()
        {

        }

        public static void FileChanged()
        {
            unsavedChanges = true;
        }

        private static void AutocompleteNeedsModified(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.PropertyDescriptorAdded:
                case ListChangedType.PropertyDescriptorChanged:
                case ListChangedType.PropertyDescriptorDeleted:
                case ListChangedType.Reset:

                //These might possibly be handled more efficiently
                case ListChangedType.ItemMoved:
                case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                    //Rebuild autocomplete list
                    AutocompleteItemSource.Clear();
                    for (int c = 0; c < openItems.Count; c++)
                    {
                        if (FileIsItems(openFiles[c])
                            || Path.GetFileName(openFiles[c]).Equals("bionics.json"))
                        {
                            for (int d = 0; d < openItems[c].Count; d++)
                                AutocompleteItemSource.Add(openItems[c][d].Display);
                        }
                    }
                    break;

                case ListChangedType.ItemAdded:
                    //Update autocomplete list
                    AutocompleteItemSource.Add(((BindingList<ItemDataWrapper>)sender)[e.NewIndex].Display);
                    break;
            }
        }

        /// <summary>
        /// Load game files from the specified path.
        /// </summary>
        public static void LoadFiles(string path)
        {
            workspacePath = path;

            //Clear
            foreach (BindingList<ItemDataWrapper> list in openItems)
                list.ListChanged -= AutocompleteNeedsModified;
            openItems.Clear();

            //Load all JSON files in directory and subs
            openFiles = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
            for (int c = 0; c < openFiles.Length; c++)
            {
                openFiles[c] = openFiles[c].Substring(workspacePath.Length + 1);
                LoadFile(openFiles[c]);
            }
        }

        public static void ReloadFiles()
        {
            LoadFiles(workspacePath);
        }

        /// <summary>
        /// Load up items from a specified game file.
        /// </summary>
        public static void LoadFile(string path)
        {
            //Load up contents of the file
            object json = LoadJson(path);
            
            BindingList<ItemDataWrapper> newItems = new BindingList<ItemDataWrapper>();
            openItems.Add(newItems);
            string ffilename = Path.GetFileName(path);

            //Parse different formats
            if (FileIsItems(path)
                || ffilename.Equals("bionics.json")
                || ffilename.Equals("item_groups.json")
                || ffilename.Equals("materials.json")
                || ffilename.Equals("monstergroups.json")
                || ffilename.Equals("names.json")
                || ffilename.Equals("professions.json"))
            {
                foreach (Dictionary<string, object> item in (object[])json)
                    newItems.Add(new ItemDataWrapper(item));
            }
            else if (ffilename.Equals("recipes.json"))
            {
                foreach (Dictionary<string, object> recipe in (object[])((Dictionary<string, object>)json)["recipes"])
                    newItems.Add(new ItemDataWrapper(recipe));

                //Also load cats
                CraftCategories = (object[])((Dictionary<string, object>)json)["categories"];
            }

            //Subscribe to events
            if (FileIsItems(path)
                || ffilename.Equals("bionics.json"))
                newItems.ListChanged += AutocompleteNeedsModified;

            //Rebuild autocomplete
            AutocompleteNeedsModified(newItems, new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        public static void SelectFile(int file)
        {
            currentFileIndex = file;
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

            string ffilename = Path.GetFileName(CurrentFileName);
            if (CurrentFileIsItems)
                WinformsUtil.ControlsLoadItem(Form1.Instance.GenericItemControl.Controls[0], CurrentItemData);
            else if (ffilename.Equals("item_groups.json"))
                WinformsUtil.ControlsLoadItem(Form1.Instance.ItemGroupControl.Controls[0], CurrentItemData);
            else if (ffilename.Equals("recipes.json"))
                WinformsUtil.ControlsLoadItem(Form1.Instance.RecipeControl.Controls[0], CurrentItemData);
        }

        public static void SaveOpenFiles()
        {
            foreach (string file in openFiles)
                SaveFile(file);
        }

        public static void SaveFile(string file)
        {
            string ffilename = Path.GetFileName(file);
            if (FileIsItems(file)
                || ffilename.Equals("bionics.json")
                || ffilename.Equals("item_groups.json")
                || ffilename.Equals("materials.json")
                || ffilename.Equals("monstergroups.json")
                || ffilename.Equals("names.json")
                || ffilename.Equals("professions.json"))
            {
                object[] serialData = new object[OpenItems.Count];
                int c = 0;
                foreach (ItemDataWrapper v in OpenItems)
                {
                    serialData[c] = v.data;
                    c++;
                }
                SaveJson(file, serialData);
            }
            else if (ffilename.Equals("recipes.json"))
            {
                object[] serialData = new object[OpenItems.Count];
                int c = 0;
                foreach (ItemDataWrapper v in OpenItems)
                {
                    serialData[c] = v.data;
                    c++;
                }
                Dictionary<string, object> serial2 = new Dictionary<string, object>();
                serial2["recipes"] = serialData;
                serial2["categories"] = CraftCategories;
                SaveJson(file, serial2);
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
                if (CurrentItemData.ContainsKey(key))
                    CurrentItemData.Remove(key);
            }
            else if (!CurrentItemData.ContainsKey(key) || value != CurrentItemData[key])
            {
                CurrentItemData[key] = value;
                unsavedChanges = true;
            }

            openItems[currentFileIndex][currentFileIndex].NotifyKeyChanged(key);
        }

        public static void LoadMaterials(ListBox box)
        {
            box.Items.Clear();
            for (int c = 0; c < openFiles.Length; c++)
            {
                if (Path.GetFileName(openFiles[c]).Equals("materials.json"))
                {
                    foreach (ItemDataWrapper item in openItems[c])
                    {
                        box.Items.Add(item.data["ident"]);
                    }
                    return;
                }
            }
        }

        public static void LoadSkills(ComboBox box)
        {
            //TODO: don't reload these files all the time
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
            //TODO: don't reload these files all the time
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

        public static void LoadCraftCategories(ComboBox box)
        {
            box.Items.Clear();
            foreach (string cc in CraftCategories)
            {
                box.Items.Add(cc);
            }
        }
    }
}
