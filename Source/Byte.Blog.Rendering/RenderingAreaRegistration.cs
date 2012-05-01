using System.Web.Mvc;
using Byte.Blog.Framework.Web;
using Byte.Blog.Rendering.Models;

namespace Byte.Blog.Rendering
{
    public class RenderingAreaRegistration : AreaRegistrationBase
    {
        public override string AreaName
        {
            get { return "Rendering"; }
        }

        protected override void RegisterRoutes(AreaRegistrationContext context)
        {
            var routeRegistrar = new RouteRegistrar(context);

            routeRegistrar.RegisterRoutes();
        }

        protected override void RegisterMappings()
        {
            AutoMapperConfig.RegisterMappings();
        }
    }
}
