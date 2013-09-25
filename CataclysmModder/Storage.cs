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
    /// Wraps data loaded from one JSON item for display in a listbox.
    /// </summary>
    class ItemDataWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Reference to the openfiles array.
        /// </summary>
        public int memberOf;

        private CataFile MemberOf
        {
            get { return Storage.GetFileDefForOpenFile(memberOf); }
        }

        public void NotifyKeyChanged(string key)
        {
            Modified = true;

            if (key.Equals(MemberOf.displayMember) || key.Equals(MemberOf.displaySuffix))
                PropertyChanged(this, new PropertyChangedEventArgs("Display"));
        }

        /// <summary>
        /// Get the list of keys from this item.
        /// </summary>
        public Dictionary<string, object> data
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the listbox display name of this item
        /// </summary>
        public string Display
        {
            get
            {
                string suffix = "";
                if (data.ContainsKey(MemberOf.displaySuffix))
                    suffix = (string)data[MemberOf.displaySuffix];

                if (data.ContainsKey(MemberOf.displayMember))
                    return (string)data[MemberOf.displayMember] + suffix;
                else
                    return "[item]";
            }
        }

        /// <summary>
        /// Tracks whether this item has unsaved changes.
        /// </summary>
        public bool Modified = false;


        /// <summary>
        /// Create a new item with data copied from the specified item.
        /// </summary>
        /// <param name="copy"></param>
        public ItemDataWrapper(ItemDataWrapper copy)
        {
            this.memberOf = copy.memberOf;

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

            //TODO: support raw arrays (skills)
            data[MemberOf.displayMember] = copy.Display + (lastitem + 1);
        }

        /// <summary>
        /// Create a new item from loaded data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="memberOf"></param>
        public ItemDataWrapper(Dictionary<string, object> data, int memberOf)
        {
            this.memberOf = memberOf;
            this.data = data;
        }

        /// <summary>
        /// Create a new, empty item.
        /// </summary>
        /// <param name="memberOf"></param>
        public ItemDataWrapper(int memberOf)
        {
            this.memberOf = memberOf;

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

            //TODO: support raw arrays (skills)
            data[MemberOf.displayMember] = "newitem" + (lastitem + 1);
        }
    }


    /// <summary>
    /// This class holds the backing data for all loaded items and handles reading and writing JSON.
    /// </summary>
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
                    return string.Empty;
            }
        }
        public static int CurrentFileIndex
        {
            get
            {
                return currentFileIndex;
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

        public static CataFile GetFileDefForCurrentFile()
        {
            return fileDef[(int)GetFileTypeForCurrentFile()];
        }

        public static CataFile GetFileDefForOpenFile(int index)
        {
            return fileDef[(int)GetFileTypeForOpenFile(index)];
        }

        public static FileType GetFileTypeForCurrentFile()
        {
            return GetFileTypeForOpenFile(currentFileIndex);
        }

        public static FileType GetFileTypeForOpenFile(int index)
        {
            if (index < 0 || index >= openFiles.Length)
                return FileType.NONE;
            else
                return GetFileType(openFiles[index]);
        }

        public static FileType GetFileType(string name)
        {
            string filename = Path.GetFileName(name);
            string[] filedirs = name.Split(Path.DirectorySeparatorChar);
            if (filedirs.Length > 1 && filedirs[filedirs.Length - 2].Equals("items"))
                return FileType.ITEMS;
            else if (filename.Equals("bionics.json"))
                return FileType.BIONICS;
            else if (filename.Equals("item_groups.json"))
                return FileType.ITEM_GROUPS;
            else if (filename.Equals("materials.json"))
                return FileType.MATERIALS;
            else if (filename.Equals("monstergroups.json"))
                return FileType.MONSTER_GROUPS;
            else if (filename.Equals("names.json"))
                return FileType.NAMES;
            else if (filename.Equals("professions.json"))
                return FileType.PROFESSIONS;
            else if (filename.Equals("recipes.json"))
                return FileType.RECIPES;
            else if (filename.Equals("skills.json"))
                return FileType.SKILLS;
            else if (filename.Equals("snippets.json"))
                return FileType.SNIPPETS;
            else if (filename.Equals("dreams.json"))
                return FileType.DREAMS;
            else if (filename.Equals("mutations.json"))
                return FileType.MUTATIONS;
            else if (filename.Equals("martialarts.json"))
                return FileType.MARTIAL_ARTS;
            else if (filename.Equals("techniques.json"))
                return FileType.TECHNIQUES;
            else if (filename.Equals("vehicle_parts.json"))
                return FileType.VEHICLE_PARTS;
            else if (filename.Equals("vehicles.json"))
                return FileType.VEHICLES;
            else
                return FileType.NONE;
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

        public static object[] CraftCategories = new object[0];
        private static List<object> RecipeUnknown = new List<object>();

        public static AutoCompleteStringCollection AutocompleteItemSource = new AutoCompleteStringCollection();
        public static AutoCompleteStringCollection AutocompleteBookSource = new AutoCompleteStringCollection();


        public enum FileType
        {
            ITEMS,
            BIONICS,
            ITEM_GROUPS,
            MATERIALS,
            MONSTER_GROUPS,
            NAMES,
            PROFESSIONS,
            RECIPES,
            SKILLS,
            SNIPPETS,
            DREAMS,
            MUTATIONS,
            MARTIAL_ARTS,
            TECHNIQUES,
            VEHICLE_PARTS,
            VEHICLES,
            NONE,

            COUNT
        }

        /// <summary>
        /// Contains information about the structure of different files.
        /// </summary>
        private static CataFile[] fileDef = new CataFile[(int)FileType.COUNT];

        //HACK: placement
        public static void HideAllControls()
        {
            foreach (CataFile f in fileDef)
            {
                if (f != null && f.control != null)
                    f.control.Visible = false;
            }
        }


        /// <summary>
        /// Initialize structures that describe how each different type of file should be read.
        /// </summary>
        public static void InitializeFileDefs()
        {
            //Editing control needs to be set in Form1 ctor using FileDefSetControl

            fileDef[(int)FileType.ITEMS] = new CataFile(
                "id",
                new JsonSchema("CataclysmModder.schemas.items.txt"));
            fileDef[(int)FileType.BIONICS] = new CataFile("id");
            fileDef[(int)FileType.ITEM_GROUPS] = new CataFile(
                "id",
                new JsonSchema("CataclysmModder.schemas.item_group.txt"));
            fileDef[(int)FileType.MATERIALS] = new CataFile("ident");
            fileDef[(int)FileType.MONSTER_GROUPS] = new CataFile("name");
            fileDef[(int)FileType.NAMES] = new CataFile("name");
            fileDef[(int)FileType.PROFESSIONS] = new CataFile(
                "ident",
                new JsonSchema("CataclysmModder.schemas.professions.txt"));
            fileDef[(int)FileType.RECIPES] = new CataFile(
                "result",
                "id_suffix",
                new JsonSchema("CataclysmModder.schemas.recipes.txt"));
            fileDef[(int)FileType.SKILLS] = new CataFile("ident");
            fileDef[(int)FileType.SNIPPETS] = null;
            fileDef[(int)FileType.NONE] = null;
            fileDef[(int)FileType.DREAMS] = null;
            fileDef[(int)FileType.MUTATIONS] = new CataFile("id");
            fileDef[(int)FileType.MARTIAL_ARTS] = new CataFile("id");
            fileDef[(int)FileType.TECHNIQUES] = new CataFile("id");
            fileDef[(int)FileType.VEHICLE_PARTS] = new CataFile("id");
            fileDef[(int)FileType.VEHICLES] = new CataFile("id");
        }

        public static void FileDefSetControl(FileType type, Control control)
        {
            fileDef[(int)type].control = control;
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
                        if (GetFileTypeForOpenFile(c) == FileType.ITEMS
                            || GetFileTypeForOpenFile(c) == FileType.BIONICS)
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
            try
            {
                openFiles = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
                for (int c = 0; c < openFiles.Length; c++)
                {
                    openFiles[c] = openFiles[c].Substring(workspacePath.Length + 1);
                    LoadFile(c);
                }
            }
            catch (DirectoryNotFoundException)
            {
                openFiles = new string[0];
            }
        }

        public static void ReloadFiles()
        {
            LoadFiles(workspacePath);
        }

        /// <summary>
        /// Load up items from a specified game file.
        /// </summary>
        public static void LoadFile(int index)
        {
            BindingList<ItemDataWrapper> newItems = new BindingList<ItemDataWrapper>();
            openItems.Add(newItems);

            //Load up contents of the file
            string path = openFiles[index];
            object json = LoadJson(path);
            if (json == null)
                return;

            FileType ftype = GetFileType(path);
            if (fileDef[(int)ftype] == null)
            {
                //Not supported

            }
            else if (ftype != FileType.NONE)
            {
                //Default parsing
                foreach (Dictionary<string, object> item in (object[])json)
                    newItems.Add(new ItemDataWrapper(item, index));
            }

            if (ftype == FileType.RECIPES)
            {
                //Remove categories
                List<string> craftcats = new List<string>();
                foreach (ItemDataWrapper c in newItems)
                {
                    if (c.data["type"].Equals("recipe_category"))
                        craftcats.Add((string)c.data["type"]);
                }
                CraftCategories = craftcats.ToArray();

                //Remove items with unknown type
                RecipeUnknown.Clear();
                for (int c = newItems.Count - 1; c >= 0; c--)
                    if (!newItems[c].data["type"].Equals("recipe"))
                    {
                        RecipeUnknown.Add(newItems[c]);
                        newItems.RemoveAt(c);
                    }
            }

            //Subscribe to events
            if (GetFileTypeForOpenFile(index) == FileType.ITEMS
                || GetFileTypeForOpenFile(index) == FileType.BIONICS)
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
            StreamWriter write = new StreamWriter(new FileStream(Path.Combine(workspacePath, file), FileMode.Create));
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
            StreamWriter write = new StreamWriter(new FileStream(Path.Combine(workspacePath, file), FileMode.Create));
            try
            {
                StringBuilder sb = new StringBuilder("{\"categories\":[");

                //Unknown stuff
                foreach (Dictionary<string, object> item in RecipeUnknown)
                {
                    string type = (item.ContainsKey(pivotKey) ? (string)item[pivotKey] : "");
                    sb.Append(schema.Serialize(item, type));
                    sb.Append(",");
                }

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

        public static void TestAllItems()
        {
            for (int c = 0; c < openFiles.Length; c++)
            {
                CataFile fileDef = GetFileDefForOpenFile(c);
                if (fileDef != null && fileDef.control != null)
                {
                    for (int d = 0; d < openItems[c].Count; d++)
                        WinformsUtil.ControlsLoadItem(fileDef.control, openItems[c][d].data);
                }
            }
            MessageBox.Show("Done");
        }

        /// <summary>
        /// Load up one item from current file's JSON.
        /// </summary>
        /// <param name="id"></param>
        public static void LoadItem(int index)
        {
            if (index < 0) return;

            currentItemIndex = index;

            CataFile fileDef = GetFileDefForOpenFile(currentFileIndex);
            if (fileDef != null && fileDef.control != null)
                WinformsUtil.ControlsLoadItem(fileDef.control, CurrentItemData);
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
            FileType ftype = GetFileTypeForCurrentFile();

            object[] serialData = new object[indices.Length];
            int c = 0;
            foreach (int i in indices)
            {
                serialData[c] = OpenItems[i].data;
                c++;
            }

            if (!Serialize(serialData, file, ftype))
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
            FileType ftype = GetFileTypeForOpenFile(fileIndex);

            //Put data into serializable format
            object[] serialData = new object[openItems[fileIndex].Count];
            int d = 0;
            foreach (ItemDataWrapper v in openItems[fileIndex])
            {
                serialData[d] = v.data;
                d++;
            }

            if (!Serialize(serialData, file, ftype))
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

        private static bool Serialize(object[] serialData, string file, FileType ftype)
        {
            if (ftype == FileType.RECIPES)
            {
                SaveJsonRecipes(file, serialData, fileDef[(int)ftype].schema, "");
            }
            else if (ftype == FileType.ITEMS)
            {
                SaveJsonItem(file, serialData, fileDef[(int)ftype].schema, "type");
            }
            else if (ftype == FileType.ITEM_GROUPS)
            {
                SaveJsonItem(file, serialData, fileDef[(int)ftype].schema, "", 2);
            }
            else if (ftype != FileType.NONE
                && fileDef[(int)ftype] != null && fileDef[(int)ftype].schema != null)
            {
                SaveJsonItem(file, serialData, fileDef[(int)ftype].schema, "");
            }
            else
            {
                return false;
            }
            return true;
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

        #region Data Source Getters

        public static string[] GetMaterialNames()
        {
            for (int c = 0; c < openFiles.Length; c++)
            {
                if (GetFileType(Path.GetFileName(openFiles[c])) == FileType.MATERIALS)
                {
                    string[] ret = new string[openItems[c].Count];
                    for (int d = 0; d < openItems[c].Count; d++)
                        ret[d] = (string)openItems[c][d].data["ident"];
                    return ret;
                }
            }
            return new string[0];
        }

        public static string[] GetTechniques()
        {
            for (int c = 0; c < openFiles.Length; c++)
            {
                if (GetFileType(Path.GetFileName(openFiles[c])) == FileType.TECHNIQUES)
                {
                    string[] ret = new string[openItems[c].Count];
                    for (int d = 0; d < openItems[c].Count; d++)
                        ret[d] = (string)openItems[c][d].data["id"];
                    return ret;
                }
            }
            return new string[0];
        }

        public static string[] GetSkills()
        {
            for (int c = 0; c < openFiles.Length; c++)
            {
                if (GetFileType(Path.GetFileName(openFiles[c])) == FileType.SKILLS)
                {
                    string[] ret = new string[openItems[c].Count + 1];
                    ret[0] = "none";
                    for (int d = 0; d < openItems[c].Count; d++)
                        ret[d + 1] = (string)openItems[c][d].data["ident"];
                    return ret;
                }
            }
            return new string[0];
        }

        public static string[] GetGunSkills()
        {
            for (int c = 0; c < openFiles.Length; c++)
            {
                if (GetFileType(Path.GetFileName(openFiles[c])) == FileType.SKILLS)
                {
                    List<string> ret = new List<string>();
                    ret.Add("none");
                    foreach (ItemDataWrapper skill in openItems[c])
                    {
                        if (skill.data.ContainsKey("tags"))
                        {
                            foreach (string f in (object[])skill.data["tags"])
                            {
                                if (f.Equals("gun_type"))
                                {
                                    ret.Add((string)skill.data["ident"]);
                                    break;
                                }
                            }
                        }
                    }
                    return ret.ToArray();
                }
            }
            return new string[0];
        }

        public static string[] GetCraftCategories()
        {
            string[] ret = new string[CraftCategories.Length];
            CraftCategories.CopyTo(ret, 0);
            return ret;
        }

        public static string[] GetAddictions()
        {
            //TODO: load from game data
            string[] ret = new string[8];
            ret[0] = "nicotine";
            ret[1] = "caffeine";
            ret[2] = "alcohol";
            ret[3] = "sleeping pill";
            ret[4] = "opiate";
            ret[5] = "amphetamine";
            ret[6] = "cocaine";
            ret[7] = "crack";
            return ret;
        }

        #endregion
    }
}
