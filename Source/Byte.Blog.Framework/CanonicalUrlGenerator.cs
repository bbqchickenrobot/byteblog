using System;
using System.Web;
using System.Web.Mvc;
using Byte.Blog.Framework.Web;

namespace Byte.Blog.Framework
{
    public class CanonicalUrlGenerator
    {
        private readonly HttpRequest request;
        private readonly UrlHelper urlHelper;

        public CanonicalUrlGenerator(HttpRequest request)
        {
            this.request = request;
            this.urlHelper = new UrlHelper(this.request.RequestContext);
        }

        public string FromRouteValues(string action, string controller, object routeValues)
        {
            var uriBuilder = new UriBuilder();

            uriBuilder.Scheme = Uri.UriSchemeHttp;
            uriBuilder.Host = this.request.Url.Host;

            if (MvcApplication.EnvironmentType == EnvironmentType.Development)
            {
                uriBuilder.Port = this.request.Url.Port;
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
