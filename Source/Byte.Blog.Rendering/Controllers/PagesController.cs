using System.Web.Mvc;
using Byte.Blog.Framework.Data;
using Byte.Blog.Rendering.Models;
using Raven.Client;

namespace Byte.Blog.Rendering.Controllers
{
    public class PagesController : RavenController
    {
        public PagesController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult ViewPage(string pageSlug)
        {
            var pageFactory = new PageFactory(this.session);
            var page = pageFactory.CreateFromSlug(pageSlug);
            if (page == null)
            {
                return new HttpNotFoundResult("No such page.");
            }

            var pageToPageViewModelMapper = new PageToPageViewModelMapper(this.session, this.Url);
            var pageViewModel = pageToPageViewModelMapper.Map(page);

            return this.View(pageViewModel);
        }
    }
}
