using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework;
using Raven.Client;

namespace Byte.Blog.Editorial.Models
{
    public class PageEditModelToPageMapper
    {
        private readonly IDocumentSession session;
        private readonly SlugMaker slugMaker;

        public PageEditModelToPageMapper(
            IDocumentSession session,
            SlugMaker slugMaker)
        {
            this.session = session;
            this.slugMaker = slugMaker;
        }

        public Page Map(PageEditModel editModel)
        {
            var page = Mapper.Map<Page>(editModel);

            page.Slug = this.slugMaker.CreateSlug(page.Title);

            return page;
        }
    }
}