using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework;
using Byte.Blog.Framework.Web;
using Raven.Client.Document;

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
