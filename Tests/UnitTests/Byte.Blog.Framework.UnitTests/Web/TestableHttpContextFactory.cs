using System;
using System.Collections.Specialized;
using System.Web;
using Moq;

namespace Byte.Blog.Framework.UnitTests.Web
{
    public class TestableHttpContextFactory
    {
        private readonly Uri requestUri;

        public TestableHttpContextFactory(Uri requestUri)
        {
            this.requestUri = requestUri;
        }

        public HttpContextBase GetMockHttpContextBase()
        {
            var httpRequestBase = this.GetMockHttpRequestBase();
            var httpResponseBase = this.GetMockHttpResponseBase();

            var context = new Mock<HttpContextBase>();

            context.SetupGet(x => x.Request).Returns(httpRequestBase);
            context.SetupGet(x => x.Response).Returns(httpResponseBase);

            return context.Object;
        }

        private HttpRequestBase GetMockHttpRequestBase()
        {
            var httpRequestBase = new Mock<HttpRequestBase>();

            httpRequestBase.SetupGet(x => x.ApplicationPath).Returns("/");
            httpRequestBase.SetupGet(x => x.Url).Returns(this.requestUri);
            httpRequestBase.SetupGet(x => x.ServerVariables).Returns(new NameValueCollection());

            return httpRequestBase.Object;
        }

        private HttpResponseBase GetMockHttpResponseBase()
        {
            var httpResponseBase = new Mock<HttpResponseBase>();
            return httpResponseBase.Object;
        }
    }
}
