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
        public string displayMember;
        public string displaySuffix = "";

        /// <summary>
        /// The premade editor control for this file type.
        /// </summary>
        public Control control;
    }
}
