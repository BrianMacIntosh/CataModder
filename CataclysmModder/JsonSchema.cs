using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace CataclysmModder
{
    class JsonSchema
    {
        private class SchemaKey
        {
            public string key;
            public bool mandatory;

            public SchemaKey(string key, bool mandatory)
            {
                this.key = key.ToLower();
                this.mandatory = mandatory;
            }
        }

        private Dictionary<string, List<SchemaKey>> keyGroups;

        public JsonSchema(string resourceName)
        {
            keyGroups = new Dictionary<string, List<SchemaKey>>();

            StreamReader reader = new StreamReader(
                Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName));
            List<SchemaKey> currentGroup = new List<SchemaKey>();
            string currentKey = "";
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string linetrim = line.Trim();
                if (linetrim.Length == 0)
                {

                }
                else if (linetrim.EndsWith(":"))
                {
                    keyGroups.Add(currentKey, currentGroup);
                    currentGroup = new List<SchemaKey>();
                    currentKey = linetrim.Substring(0, linetrim.Length - 1);
                }
                else if (linetrim.StartsWith("<") && linetrim.EndsWith(">"))
                {
                    currentGroup.Add(new SchemaKey(linetrim.Substring(1, linetrim.Length - 2), false));
                }
                else
                {
                    currentGroup.Add(new SchemaKey(linetrim, true));
                }
            }
            keyGroups.Add(currentKey, currentGroup);
            reader.Close();
        }

        public string Serialize(Dictionary<string, object> data, string typeKey)
        {
            StringBuilder builder = new StringBuilder("{");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (SchemaKey key in GetKeysForType(typeKey))
            {
                if (data.ContainsKey(key.key))
                    builder.Append("\"" + key.key + "\":" + serializer.Serialize(data[key.key]) + ",");
            }
            //Remove last comma
            builder.Remove(builder.Length - 1, 1);

            builder.Append("}");
            return builder.ToString();
        }

        private List<SchemaKey> GetKeysForType(string typekey)
        {
            List<SchemaKey> ret = new List<SchemaKey>();
            if (keyGroups.ContainsKey(""))
                ret.AddRange(keyGroups[""]);
            if (!typekey.Equals(string.Empty))
                ret.AddRange(keyGroups[typekey]);
            return ret;
        }
    }
}
