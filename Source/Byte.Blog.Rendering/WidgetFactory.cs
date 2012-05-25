using System.Collections.Generic;
using System.Linq;
using Byte.Blog.Content;
using Raven.Client;

namespace Byte.Blog.Rendering
{
    public class WidgetFactory
    {
        private readonly IDocumentSession session;

        public WidgetFactory(IDocumentSession session)
        {
            this.session = session;
        }

        public IEnumerable<Widget> GetWidgets()
        {
            var databaseWidgets = this.session.Query<Widget>().ToList();

            databaseWidgets.Add(Widget.AboutWidget);
            databaseWidgets.Add(Widget.StackoverflowWidget);

            return databaseWidgets;
        }
    }
}