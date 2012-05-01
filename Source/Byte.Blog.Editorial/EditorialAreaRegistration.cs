using System.Web.Mvc;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework.Web;

namespace Byte.Blog.Editorial
{
    public class EditorialAreaRegistration : AreaRegistrationBase
    {
        public override string AreaName
        {
            get { return "Editorial"; }
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
