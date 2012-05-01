using System;
using System.Collections.ObjectModel;

namespace Byte.Blog.Content
{
    public class Entry
    {
        public static readonly string IdPrefix = "entries/";

        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset? PublishedAtUtc { get; set; }
        public DateTimeOffset LastModifiedAtUtc { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string PageId { get; set; }
        public Collection<string> Tags { get; set; }
        public Collection<string> References { get; set; }

        public Entry() 
            : this(IdPrefix)
        {
        }

        public Entry(string id) 
        {
            this.Id = id;
            this.Tags = new Collection<string>();
            this.References = new Collection<string>();
        }
    }
}
