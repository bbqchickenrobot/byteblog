using System.Web.Mvc;
using Raven.Client;

namespace Byte.Blog.Framework.Data
{
    public abstract class RavenController : Controller
    {
        private readonly IDocumentStore documentStore;

        protected IDocumentSession session { get; private set; }

        protected RavenController(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.session = this.documentStore.OpenSession();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            using (this.session)
            {
                if (filterContext.Exception != null)
                {
                    return;
                }

                if (this.session != null)
                {
                    this.session.SaveChanges();
                }
            }
        }
    }
}
