using System;
using System.Web.Mvc;
using Byte.Blog.Framework.Web;

namespace Byte.Blog.Framework
{
    public class CanonicalUrlGenerator
    {
        //TODO: get from configuration
        private const string DevelopmentHostname = "localhost";
        private const int DevelopmentPort = 5000;
        private const string ProductionHostname = "byteblog.apphb.com";

        private readonly UrlHelper urlHelper;

        public CanonicalUrlGenerator(UrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public string FromRouteValues(string action, string controller, object routeValues)
        {
            var uriBuilder = new UriBuilder();

            uriBuilder.Scheme = Uri.UriSchemeHttp;

            if (MvcApplication.EnvironmentType == EnvironmentType.Development)
            {
                uriBuilder.Host = DevelopmentHostname;
                uriBuilder.Port = DevelopmentPort;
            }
            else
            {
                uriBuilder.Host = ProductionHostname;
            }

            uriBuilder.Path = this.GetPath(action, controller, routeValues);;

            return uriBuilder.ToString();
        }

        private string GetPath(string action, string controller, object routeValues)
        {
            return this.urlHelper.Action(
                action,
                controller,
                routeValues);
        }
    }
}
