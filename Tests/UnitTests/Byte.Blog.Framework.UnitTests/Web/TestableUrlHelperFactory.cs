using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Byte.Blog.Framework.UnitTests.Web
{
    public class TestableUrlHelperFactory
    {
        private readonly Uri requestUri;

        public TestableUrlHelperFactory(Uri requestUri = null)
        {
            if (requestUri == null)
            {
                this.requestUri = new Uri("http://localhost");
            }
            else
            {
                this.requestUri = requestUri;
            }
        }

        public UrlHelper Create(RouteCollection routeCollection = null, RouteData routeData = null)
        {
            var testableHttpContextFactory = new TestableHttpContextFactory(this.requestUri);

            var httpContextBase = testableHttpContextFactory.GetMockHttpContextBase();

            if (routeData == null)
            {
                routeData = new RouteData();
            }

            var requestContext = new RequestContext(httpContextBase, routeData);

            if (routeCollection != null)
            {
                return new UrlHelper(requestContext, routeCollection);
            }

            return new UrlHelper(requestContext);
        }
    }
}
