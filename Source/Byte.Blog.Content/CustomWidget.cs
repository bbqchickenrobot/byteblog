namespace Byte.Blog.Content
{
    public class CustomWidget : Widget
    {
        public static readonly CustomWidget StackoverflowCustomWidget = new CustomWidget
        {
            Markup = @"
<a href=""http://stackoverflow.com/users/112765/byte"">
<img src=""http://stackoverflow.com/users/flair/112765.png"" width=""208"" height=""58"" alt=""profile for byte at Stack Overflow, Q&amp;A for professional and enthusiast programmers"" title=""profile for byte at Stack Overflow, Q&amp;A for professional and enthusiast programmers"">
</a>"
        };

        public static readonly CustomWidget AboutCustomWidget = new CustomWidget
        {
            Markup = "<p>Ben Lakey is a software developer for msnbc.com, an avid gamer, and Seattle resident. The views expressed here are my own and do not necessarily reflect those of my employer.</p>"
        };

        public string Markup { get; set; }
    }
}