using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Byte.Blog.Rendering.Models;

namespace Byte.Blog.Rendering
{
    internal class CanonicalUrlFactory
    {
        private readonly RequestContext requestContext;

        public CanonicalUrlFactory(RequestContext requestContext)
        {
            this.requestContext = requestContext;
        }

        public string FromEntryViewModel(EntryViewModel entry)
        {
            var urlHelper = new UrlHelper(this.requestContext);
            
            var relativeUrl = urlHelper.Action(
                "ViewEntry", 
                "Entries", 
                new 
                { 
                    pageSlug = entry.PageSlug, 
                    entrySlug = entry.Slug 
                });

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Uri.UriSchemeHttp);
            stringBuilder.Append(Uri.SchemeDelimiter);
            stringBuilder.Append(this.requestContext.HttpContext.Request.Url.Authority);
            stringBuilder.Append(relativeUrl);

            return stringBuilder.ToString();
        }
    }
}
