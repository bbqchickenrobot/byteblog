
namespace Byte.Blog.Editorial.Models
{
    public class HtmlColor
    {
        public static HtmlColor[] AllColors = new HtmlColor[]
        {
            new HtmlColor("Aqua","#48D8FF"),
            new HtmlColor("Purple","#A53AF3"),
            new HtmlColor("Magenta","#FF0F8B"),
            new HtmlColor("Red","#FF250D"),
            new HtmlColor("Green","#19B917"),
            new HtmlColor("Yellow","#FED20A"),
            new HtmlColor("Blue","#2F6EFC")
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
