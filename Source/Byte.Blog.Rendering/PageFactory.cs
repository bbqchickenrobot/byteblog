using System.Linq;
using Byte.Blog.Content;
using Raven.Client;

namespace Byte.Blog.Rendering
{
    public class PageFactory
    {
        private readonly IDocumentSession session;

        public PageFactory(IDocumentSession session)
        {
            this.session = session;
        }

        public Page CreateFromSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug) || slug == Page.HomePage.Slug)
            {
                return Page.HomePage;
            }

            var page = this.session.Query<Page>()
                .FirstOrDefault(p => p.Slug == slug && !p.Deleted);

            return page;
        }
    }
}
