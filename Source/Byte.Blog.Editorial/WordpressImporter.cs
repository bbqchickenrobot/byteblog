using System.Linq;
using System.Xml.Linq;
using Raven.Client;

namespace Byte.Blog.Editorial
{
    public class WordpressImporter
    {
        private IDocumentSession session;

        public WordpressImporter(IDocumentSession session)
        {
            this.session = session;
        }

        public WordpressImportStatistics ImportFromXml(XDocument document)
        {
            var parsedBlog = new PressSharp.Blog(document);

            var posts = parsedBlog.GetPosts();

            //TODO: save to db

            return new WordpressImportStatistics()
            {
                EntriesCount = posts.Count(),
                PagesCount = parsedBlog.Categories.Count(),
                TagsCount = parsedBlog.Tags.Count()
            };
        }
    }
}
