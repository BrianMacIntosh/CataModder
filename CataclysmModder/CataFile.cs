using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CataclysmModder
{
    class CataFile
    {
        /// <summary>
        /// Internal content path to a schema describing how to save this file.
        /// </summary>
        public JsonSchema schema;

        /// <summary>
        /// The key to use for displaying items from this file in the listbox.
        /// </summary>
        public string displayMember = null;
        public string displaySuffix = "";

        /// <summary>
        /// The premade editor control for this file type.
        /// </summary>
        public Control control;


        public CataFile(string displayMember)
        {
            this.displayMember = displayMember;
        }

        public CataFile(string displayMember, JsonSchema schema)
            : this(displayMember)
        {
            this.schema = schema;
        }

        public CataFile(string displayMember, string displaySuffix, JsonSchema schema)
            : this(displayMember, schema)
        {
            this.displaySuffix = displaySuffix;
        }
    }
}
