using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Framework;
using Byte.Blog.Framework.Web;
using Byte.Blog.Rendering.Models;
using Raven.Client.Document;

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

        protected override DocumentConvention CreateDocumentConventions()
        {
            return new DocumentConvention
            {
                FindTypeTagName = type =>
                {
                    if (typeof(Widget).IsAssignableFrom(type))
                    {
                        return Widget.IdPrefix.TrimSuffix("/");
                    }
                    return DocumentConvention.DefaultTypeTagName(type);
                }
            };
        }
    }
}
