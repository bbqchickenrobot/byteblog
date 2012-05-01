using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework;
using Byte.Blog.Framework.Data;
using Microsoft.Web.Mvc;
using Raven.Client;

namespace Byte.Blog.Editorial.Controllers
{
    public class PagesController : RavenController
    {
        public PagesController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult New()
        {
            var page = new Page();

            var mapper = new PageToPageEditModelMapper(this.session);
            var pageEditModel = mapper.Map(page);

            return this.View("Edit", pageEditModel);
        }

        public ActionResult Edit(string pageId)
        {
            pageId = pageId.EnsurePrefix(Page.IdPrefix);

            var page = this.session.Load<Page>(pageId);
            if(page == null)
            {
                this.ModelState.AddModelError("No Such Page", "There was no page found matching the id.");

                return this.View(new PageEditModel());
            }

            var mapper = new PageToPageEditModelMapper(this.session);
            var pageEditModel = mapper.Map(page);

            return this.View(pageEditModel);
        }

        [AjaxOnly]
        public ActionResult Save(PageEditModel editModel)
        {
            if(!this.ModelState.IsValid)
            {
                return this.Json(new { error = true });
            }

            var mapper = new PageEditModelToPageMapper(this.session);
            var page = mapper.Map(editModel);

            this.session.Store(page);
            this.session.SaveChanges();

            return this.Json(page);
        }

        //TODO: 'REST' 'DELETE' ?
        [HttpPost]
        public ActionResult Delete(string pageId)
        {
            pageId = pageId.EnsurePrefix(Page.IdPrefix);

            var page = this.session.Load<Page>(pageId);
            page.Deleted = true;

            this.session.SaveChanges();

            return this.Json(new { });
        }
    }
}
