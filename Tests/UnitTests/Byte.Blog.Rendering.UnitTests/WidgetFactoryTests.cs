using Byte.Blog.Content;
using Byte.Blog.Framework;
using Byte.Blog.Framework.UnitTests;
using Raven.Client.Document;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests
{
    public class WidgetFactoryTests
    {
        [Fact]
        public void Widgets_in_database_are_produced_by_factory()
        {
            var conventions = new DocumentConvention
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

            var testableStore = new TestableStore(conventions);

            var fooWidget = new CustomWidget { Markup = "foo" };
            var barWidget = new CustomWidget { Markup = "bar" };

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
