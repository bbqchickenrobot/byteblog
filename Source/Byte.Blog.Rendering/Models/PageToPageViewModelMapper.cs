using AutoMapper;
using Byte.Blog.Content;

namespace Byte.Blog.Rendering.Models
{
    public class PageToPageViewModelMapper
    {
        public PageToPageViewModelMapper()
        {
        }

        public PageViewModel Map(Page page)
        {
            return Mapper.Map<PageViewModel>(page);
        }
    }
}