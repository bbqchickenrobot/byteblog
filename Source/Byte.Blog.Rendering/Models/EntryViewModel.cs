using System;
using System.Collections.Generic;
using System.Linq;

namespace Byte.Blog.Rendering.Models
{
    public class EntryViewModel
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset? PublishedAtUtc { get; set; }
        public DateTimeOffset LastModifiedAtUtc { get; set; }
        public bool Deleted { get; set; }
        public string PageId { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> References { get; set; }

        public EntryViewModel()
        {
            this.Tags = Enumerable.Empty<string>();
            this.References = Enumerable.Empty<string>();
        }
    }
}