using System;

namespace Byte.Blog.Content
{
    public class Page
    {
        public static readonly string IdPrefix = "pages/";

        public static Page HomePage = new Page(IdPrefix + "homepage")
        {
            Slug = "home",
            Title = "Home Page",
            HtmlColor = "#FF250D",
            LastModifiedAtUtc = DateTime.UtcNow
        };

        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string HtmlColor { get; set; }
        public DateTimeOffset LastModifiedAtUtc { get; set; }
        public bool Deleted { get; set; }

        public Page() 
            : this(IdPrefix)
        {
        }

        public Page(string id) 
        {
            this.Id = id;
        }
    }
}
