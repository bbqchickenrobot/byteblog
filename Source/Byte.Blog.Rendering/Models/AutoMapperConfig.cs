using AutoMapper;
using Byte.Blog.Content;

namespace Byte.Blog.Rendering.Models
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Entry, EntryViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
