using System.Linq;
using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Framework.Data;
using Raven.Client;
using Raven.Client.Linq;

namespace Byte.Blog.Editorial.Controllers
{
    public class TagsController : RavenController
    {
        public TagsController(IDocumentStore documentStore) 
            : base(documentStore)
        {
            
        }

        public ActionResult Search(string term)
        {
            //TODO: this is just a clumsy way of searching on tags within entries. 
            //Once Tags are first-class citizens, we'll actually query on them, and entries will just have refs
            var tags = this.session.Query<Entry>()
                .Where(e => e.Tags.Any(t => t.StartsWith(term)))
                .ToList()
                .SelectMany(e => e.Tags)
                .Where(tag => tag.StartsWith(term))
                .ToList();

            //allow this new tag to be chosen in the UI
            if (!tags.Contains(term))
            {
                tags.Add(term);
            }

            var uniqueTags = tags.Distinct();

            return this.Json(uniqueTags, JsonRequestBehavior.AllowGet);
        }
    }
}
