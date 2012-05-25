using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests
{
    public class WidgetFactoryTests
    {
        [Fact]
        public void Widgets_in_database_are_produced_by_factory()
        {
            var testableStore = new TestableStore();

            var fooWidget = new Widget { Markup = "foo" };
            var barWidget = new Widget { Markup = "bar" };

            using (var session = testableStore.OpenSession())
            {
                session.Store(fooWidget);
                session.Store(barWidget);
                session.SaveChanges();

                var widgetFactory = new WidgetFactory(session);
                var widgets = widgetFactory.GetWidgets();

                Assert.Contains(fooWidget, widgets);
                Assert.Contains(barWidget, widgets);
            }
        }
    }
}
