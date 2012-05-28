using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Byte.Blog.Framework.Data;
using Byte.Blog.Rendering.Models;
using Raven.Client;

namespace Byte.Blog.Rendering.Controllers
{
    public class SidebarController : RavenController
    {
        public SidebarController(IDocumentStore documentStore) 
            : base(documentStore)
        {
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            var widgetFactory = new WidgetFactory(this.session);
            var widgets = widgetFactory.GetWidgets();

            var widgetViewModels = widgets.Select(
                w => Mapper.Map(w, w.GetType(), typeof(WidgetViewModel)) as WidgetViewModel);

            var sidebar = new SidebarViewModel
            {
                Widgets = widgetViewModels
            };

            return this.View(sidebar);
        }
    }
}
