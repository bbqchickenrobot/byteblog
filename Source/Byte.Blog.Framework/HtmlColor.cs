
namespace Byte.Blog.Framework
{
    public class HtmlColor
    {
        public static readonly HtmlColor Aqua = new HtmlColor("Aqua", "#48D8FF");
        public static readonly HtmlColor Purple = new HtmlColor("Purple","#A53AF3");
        public static readonly HtmlColor Magenta = new HtmlColor("Magenta", "#FF0F8B");
        public static readonly HtmlColor Red = new HtmlColor("Red", "#FF250D");
        public static readonly HtmlColor Green = new HtmlColor("Green", "#19B917");
        public static readonly HtmlColor Yellow = new HtmlColor("Yellow", "#FED20A");
        public static readonly HtmlColor Blue = new HtmlColor("Blue", "#2F6EFC");

        public static HtmlColor[] AllColors = new[]
        {
            Aqua,
            Purple, 
            Magenta,
            Red,
            Green,
            Yellow,
            Blue
        };

        public string FriendlyName { get; private set; }
        public string HexColor { get; private set; }

        public HtmlColor(string friendlyName, string hexColor)
        {
            this.FriendlyName = friendlyName;
            this.HexColor = hexColor;
        }
    }
}
