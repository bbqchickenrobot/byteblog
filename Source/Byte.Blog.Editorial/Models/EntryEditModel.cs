using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Byte.Blog.Content;

namespace Byte.Blog.Editorial.Models
{
    public class EntryEditModel
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Preview { get; set; }
        public DateTimeOffset? PublishedAtUtc { get; set; }
        public DateTimeOffset LastModifiedAtUtc { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        [Display(Name = "Page")]
        public string PageId { get; set; }
        public string PageTitle { get; set; }
        public string PageColor { get; set; }
        public IEnumerable<Page> PossiblePages { get; set; }
        public IList<string> Tags { get; set; }
        public IList<string> References { get; set; }

        public EntryEditModel()
        {
            this.PossiblePages = new List<Page>();
            this.Tags = new List<string>();
            this.References = new List<string>();
        }
    }
}