using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Byte.Blog.Editorial.Models
{
    public class PageEditModel
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string HtmlColor { get; set; }
        public IEnumerable<SelectListItem> PossibleHtmlColors { get; set; }
        public DateTimeOffset LastModifiedAtUtc { get; set; }
        public bool Deleted { get; set; }

        public PageEditModel()
        {
            this.PossibleHtmlColors = new List<SelectListItem>();
        }
    }
}