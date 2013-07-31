using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ContainValues : UserControl
    {
        public ContainValues()
        {
            InitializeComponent();

            containsNumeric.Tag = new JsonFormTag(
                "contains",
                "The number of items or fluid units this container holds.",
                true,
                1);

            WinformsUtil.ControlsAttachHooks(Controls[0]);
            WinformsUtil.TagsSetDefaults(Controls[0]);
        }
    }
}
