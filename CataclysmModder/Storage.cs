using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace CataclysmModder
{
    /// <summary>
    /// Wraps data loaded from a JSON item for display in a listbox.
    /// </summary>
    class ItemDataWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyKeyChanged(string key)
        {
            Modified = true;

            if (key.Equals("id_suffix"))
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Display"));
                return;
            }
            foreach (string s in DisplayMembers)
                if (s.Equals(key))
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Display"));
                    return;
                }
        }

        public static string[] DisplayMembers = new string[] { "id", "ident", "name", "result" };

        public Dictionary<string, object> data
        {
            get;
            private set;
        }

        public string Display
        {
            get
            {
                string suffix = "";
                if (data.ContainsKey("id_suffix"))
                    suffix = (string)data["id_suffix"];
                foreach (string s in DisplayMembers)
                    if (data.ContainsKey(s))
                        return (string)data[s] + suffix;
                return "[item]";
            }
        }

        public bool Modified = false;

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
        private static JsonSchema itemSchema;
        private static JsonSchema itemgroupSchema;
        private static JsonSchema recipesSchema;

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
        public static int CurrentItemIndex { get { return currentItemIndex; } }
        public static Dictionary<string, object> CurrentItemData
        {
            get
            {
                if (currentItemIndex >= 0)
                    return openItems[currentFileIndex][currentItemIndex].data;
                else
                    return null;
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
        public static AutoCompleteStringCollection AutocompleteBookSource = new AutoCompleteStringCollection();


        static Storage()
        {
            //Load schemas
            itemSchema = new JsonSchema("CataclysmModder.schemas.items.txt");
            itemgroupSchema = new JsonSchema("CataclysmModder.schemas.item_group.txt");
            recipesSchema = new JsonSchema("CataclysmModder.schemas.recipes.txt");
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
                //case ListChangedType.ItemChanged:
                case ListChangedType.ItemDeleted:
                    //Rebuild autocomplete list
                    ItemDataWrapper added;
                    AutocompleteItemSource.Clear();
                    AutocompleteBookSource.Clear();
                    for (int c = 0; c < openItems.Count; c++)
                    {
                        if (FileIsItems(openFiles[c])
                            || Path.GetFileName(openFiles[c]).Equals("bionics.json"))
                        {
                            for (int d = 0; d < openItems[c].Count; d++)
                            {
                                added = openItems[c][d];
                                if (added.data.ContainsKey("type")
                                    && added.data["type"].ToString().ToLower() == "book")
                                    AutocompleteBookSource.Add(added.Display);
                                AutocompleteItemSource.Add(added.Display);
                            }
                        }
                    }
                    break;

                case ListChangedType.ItemAdded:
                    //Update autocomplete list
                    added = ((BindingList<ItemDataWrapper>)sender)[e.NewIndex];
                    if (added.data.ContainsKey("type")
                        && added.data["type"].ToString().ToLower() == "book")
                        AutocompleteBookSource.Add(added.Display);
                    AutocompleteItemSource.Add(added.Display);
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
            if (json == null)
                return;
            
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
            catch (ArgumentException e)
            {
                MessageBox.Show("Failed to parse JSON from file '" + file + "': " + e.Message,
                    "Argument Exception", MessageBoxButtons.OK);
                return null;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Failed to parse JSON from file '" + file + "': " + e.Message,
                    "Invalid Operation Exception", MessageBoxButtons.OK);
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public static void SaveJsonItem(string file, object obj, JsonSchema schema, string pivotKey, int bracketBlockLevel = 1)
        {
            StreamWriter write = new StreamWriter(new FileStream(file, FileMode.Create));
            try
            {
                StringBuilder sb = new StringBuilder("[");
                foreach (Dictionary<string, object> item in (object[])obj)
                {
                    string type = (item.ContainsKey(pivotKey) ? (string)item[pivotKey] : "");
                    sb.Append(schema.Serialize(item, type));
                    sb.Append(",");
                }
                //Remove last comma
                sb.Remove(sb.Length - 1, 1);

                sb.Append("]");

                if (Options.DontFormatJson)
                    write.Write(sb.ToString());
                else
                    write.Write(SpaceJson(sb.ToString(), bracketBlockLevel));
            }
            catch (ArgumentException)
            {
                //TODO: error message
                return;
            }
            finally
            {
                write.Close();
            }
        }

        public static void SaveJsonRecipes(string file, object obj, JsonSchema schema, string pivotKey, int bracketBlockLevel = 3)
        {
            StreamWriter write = new StreamWriter(new FileStream(file, FileMode.Create));
            try
            {
                StringBuilder sb = new StringBuilder("{\"categories\":[");

                //Categories
                foreach (string cat in CraftCategories)
                    sb.Append('"' + cat + "\",");
                //Remove last comma
                sb.Remove(sb.Length - 1, 1);

                sb.Append("],\"recipes\":[");

                //Recipes
                foreach (Dictionary<string, object> item in (object[])obj)
                {
                    string type = (item.ContainsKey(pivotKey) ? (string)item[pivotKey] : "");
                    sb.Append(schema.Serialize(item, type));
                    sb.Append(",");
                }
                //Remove last comma
                sb.Remove(sb.Length - 1, 1);

                sb.Append("]}");

                if (Options.DontFormatJson)
                    write.Write(sb.ToString());
                else
                    write.Write(SpaceJson(sb.ToString(), bracketBlockLevel));
            }
            catch (ArgumentException)
            {
                //TODO: error message
                return;
            }
            finally
            {
                write.Close();
            }
        }

        /*public static void SaveJson(string file, object obj)
        {
            StreamWriter write = new StreamWriter(new FileStream(Path.Combine(workspacePath, file), FileMode.Create));
            try
            {
                string json = new JavaScriptSerializer().Serialize(obj);
                if (Options.DontFormatJson)
                    write.Write(json);
                else
                    write.Write(SpaceJson(json));
            }
            catch (ArgumentException)
            {
                //TODO: error message
            }
            finally
            {
                write.Close();
            }
        }*/
        
        public static string SpaceJson(string json, int bracketBlockLevel = 1)
        {
            StringBuilder newjson = new StringBuilder();
            string nlindent = "\n";

            string indent = "";
            if (Options.IndentWithTabs)
                indent = "\t";
            else
            {
                for (int c = 0; c < Options.IndentSpaces; c++)
                    indent += " ";
            }

            bool quoteOpen = false;
            bool escape = false;
            int squareBrackets = 0; //HACK: slightly
            for (int c = 0; c < json.Length; c++)
            {
                if (!escape && json[c] == '"') quoteOpen = !quoteOpen;
                if (!escape && json[c] == '\\') escape = true;
                else escape = false;

                if (!quoteOpen && (json[c] == ']' || json[c] == '}'))
                {
                    nlindent = nlindent.Substring(0, nlindent.Length-indent.Length);
                    if (squareBrackets <= bracketBlockLevel) newjson.Append(nlindent);
                    newjson.Append(json[c]);
                    if (json[c] == ']') squareBrackets--;
                }
                else if (!quoteOpen && (json[c] == '[' || json[c] == '{'))
                {
                    if (json[c] == '[') squareBrackets++;
                    nlindent += indent;
                    newjson.Append(json[c]);
                    if (squareBrackets <= bracketBlockLevel) newjson.Append(nlindent);
                }
                else if (!quoteOpen && json[c] == ',')
                {
                    newjson.Append(json[c]);
                    if (squareBrackets <= bracketBlockLevel) newjson.Append(nlindent);
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
                SaveFile(file, false);
            unsavedChanges = false;
            MessageBox.Show("All files saved.", "Saving", MessageBoxButtons.OK);
        }

        public static void ExportFile(string file, int[] indices)
        {
            string ffilename = Path.GetFileName(CurrentFileName);
            if (FileIsItems(CurrentFileName))
            {
                object[] serialData = new object[indices.Length];
                int c = 0;
                foreach (int i in indices)
                {
                    serialData[c] = OpenItems[i].data;
                    c++;
                }
                SaveJsonItem(file, serialData, itemSchema, "type");
            }
            else if (ffilename.Equals("item_groups.json"))
            {
                object[] serialData = new object[indices.Length];
                int c = 0;
                foreach (int i in indices)
                {
                    serialData[c] = OpenItems[i].data;
                    c++;
                }
                SaveJsonItem(file, serialData, itemgroupSchema, "", 2);
            }
            else if (ffilename.Equals("recipes.json"))
            {
                object[] serialData = new object[indices.Length];
                int c = 0;
                foreach (int i in indices)
                {
                    serialData[c] = OpenItems[i].data;
                    c++;
                }
                SaveJsonRecipes(file, serialData, recipesSchema, "");
            }
            else
            {
                MessageBox.Show("Serializing this file is not supported.", "Error", MessageBoxButtons.OK);
                return;
            }
        }

        public static void SaveFile(string file, bool standalone = true)
        {
            int fileIndex = -1;
            for (int c = 0; c < OpenFiles.Length; c++)
                if (file.Equals(OpenFiles[c]))
                {
                    fileIndex = c;
                    break;
                }
            if (fileIndex == -1)
            {
                //TODO: error
                return;
            }

            string ffilename = Path.GetFileName(file);
            if (FileIsItems(file))
            {
                object[] serialData = new object[openItems[fileIndex].Count];
                int c = 0;
                foreach (ItemDataWrapper v in openItems[fileIndex])
                {
                    serialData[c] = v.data;
                    c++;
                }
                SaveJsonItem(Path.Combine(workspacePath, file), serialData, itemSchema, "type");
            }
            else if (ffilename.Equals("item_groups.json"))
            {
                object[] serialData = new object[openItems[fileIndex].Count];
                int c = 0;
                foreach (ItemDataWrapper v in openItems[fileIndex])
                {
                    serialData[c] = v.data;
                    c++;
                }
                SaveJsonItem(Path.Combine(workspacePath, file), serialData, itemgroupSchema, "", 2);
            }
            else if (ffilename.Equals("recipes.json"))
            {
                object[] serialData = new object[openItems[fileIndex].Count];
                int c = 0;
                foreach (ItemDataWrapper v in openItems[fileIndex])
                {
                    serialData[c] = v.data;
                    c++;
                }
                SaveJsonRecipes(Path.Combine(workspacePath, file), serialData, recipesSchema, "");
            }
            else
            {
                if (standalone)
                    MessageBox.Show("Serializing this file is not supported.", "Error", MessageBoxButtons.OK);
                return;
            }

            //Mark items as saved
            foreach (ItemDataWrapper data in openItems[fileIndex])
                data.Modified = false;

            if (standalone)
                MessageBox.Show("File '" + file + "' saved.", "Saving", MessageBoxButtons.OK);

            //Check if other files are still outstanding
            if (standalone)
            {
                foreach (BindingList<ItemDataWrapper> list in openItems)
                    foreach (ItemDataWrapper data in list)
                        if (data.Modified)
                            return;
                unsavedChanges = false;
            }
        }

        /// <summary>
        /// For items, apply a new value to the current item.
        /// </summary>
        public static void ItemApplyValue(string key, object value, bool mandatory)
        {
            if (!mandatory &&
                (value.Equals("") ||
                value == null ||
                (value is object[] && ((object[])value).Length == 0)))
            {
                if (CurrentItemData.ContainsKey(key))
                    CurrentItemData.Remove(key);
            }
            else if (!CurrentItemData.ContainsKey(key) || value != CurrentItemData[key])
            {
                CurrentItemData[key] = value;
                unsavedChanges = true;
            }

            openItems[currentFileIndex][currentItemIndex].NotifyKeyChanged(key);
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
                    object json = LoadJson(s);
                    if (json == null)
                        return;
                    foreach (object[] item in (object[])json)
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
            foreach (string s in openFiles)
            {
                if (Path.GetFileName(s).Equals("skills.json"))
                {
                    object json = LoadJson(s);
                    if (json == null)
                        return;
                    foreach (object[] item in (object[])json)
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
