using System.Drawing;

namespace CataclysmModder
{
    static class CataColor
    {
        //TODO: use colors more accurate to game?
        public static Color GetColorFgByName(string name)
        {
            switch (name)
            {
                case "black":
                    return Color.Black;
                case "white":
                    return Color.White;
                case "light_gray":
                case "ltgray":
                    return Color.LightGray;
                case "dark_gray":
                case "dkgray":
                    return Color.DarkGray;
                case "red":
                    return Color.Red;
                case "green":
                    return Color.SpringGreen; //TODO: ...
                case "blue":
                    return Color.Blue;
                case "cyan":
                    return Color.Cyan;
                case "magenta":
                    return Color.Magenta;
                case "brown":
                    return Color.Brown;
                case "light_red":
                case "ltred":
                    return Color.PaleVioletRed; //TODO:
                case "light_green":
                case "ltgreen":
                    return Color.LightGreen;
                case "light_blue":
                case "ltblue":
                    return Color.LightBlue;
                case "light_cyan":
                case "ltcyan":
                    return Color.LightCyan;
                case "pink":
                    return Color.HotPink;
                case "yellow":
                    return Color.Yellow;
                default:
                    return Color.White;
            }
        }
    }
}
