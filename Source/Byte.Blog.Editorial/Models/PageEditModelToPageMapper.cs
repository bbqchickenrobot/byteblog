using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework;
using Raven.Client;

namespace Byte.Blog.Editorial.Models
{
    public class PageEditModelToPageMapper
    {
        private readonly IDocumentSession session;

        public PageEditModelToPageMapper(IDocumentSession session)
        {
            this.session = session;
        }

        public Page Map(PageEditModel editModel)
        {
            var page = Mapper.Map<Page>(editModel);

            page.Slug = (new SlugMaker()).CreateSlug(page.Title);

            return page;
        }
    }
}