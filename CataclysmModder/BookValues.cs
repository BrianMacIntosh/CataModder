using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class BookValues : UserControl
    {
        public BookValues()
        {
            InitializeComponent();

            skillComboBox.Tag = new JsonFormTag(
                "skill",
                "The skill this book trains.",
                true,
                "none");
            ((JsonFormTag)skillComboBox.Tag).dataSource = JsonFormTag.DataSourceType.SKILLS;
            reqlevelNumeric.Tag = new JsonFormTag(
                "required_level",
                "The minimum skill level needed to understand this book.");
            maxlevelNumeric.Tag = new JsonFormTag(
                "max_level",
                "The maximum skill level that can be achieved with this book.");
            intNumeric.Tag = new JsonFormTag(
                "intelligence",
                "The minimum intelligence needed to understand this book.");
            funNumeric.Tag = new JsonFormTag(
                "fun",
                "Morale bonus given by reading this book.");
            timeNumeric.Tag = new JsonFormTag(
                "time",
                "Time in minutes on reading session on this book takes.");

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);
        }
    }
}
