using Byte.Blog.Framework.Web;
using Microsoft.Practices.Unity;
using Xunit;

namespace Byte.Blog.Framework.UnitTests.Web
{
    public class UnityDependencyResolverTests
    {
        private interface IFoo {}
        private class Foo : IFoo {}

        [Fact]
        public void Resolver_should_return_null_when_type_requested_not_registered()
        {
            var container = new UnityContainer();
            var resolver = new UnityDependencyResolver(container);
            var resolved = resolver.GetService(typeof (IFoo));
            
            Assert.Null(resolved);
        }

        [Fact]
        public void Resolver_should_return_type_when_type_has_been_registered()
        {
            var container = new UnityContainer();
            container.RegisterType<IFoo, Foo>();

            var resolver = new UnityDependencyResolver(container);
            var resolved = resolver.GetService(typeof(IFoo));

            Assert.IsType<Foo>(resolved);
        }
    }
}
