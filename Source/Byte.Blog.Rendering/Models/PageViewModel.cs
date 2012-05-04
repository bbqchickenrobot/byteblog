
using System.Collections.Generic;

namespace Byte.Blog.Rendering.Models
{
    public class PageViewModel
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string HtmlColor { get; set; }

        public int PageNumber { get; set; }
        public IEnumerable<EntryViewModel> Entries { get; set; }
    }
}