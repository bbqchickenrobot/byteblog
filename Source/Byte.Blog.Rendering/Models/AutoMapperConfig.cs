using AutoMapper;
using Byte.Blog.Content;

namespace Byte.Blog.Rendering.Models
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Entry, EntryViewModel>();

            Mapper.CreateMap<Page, PageViewModel>()
                .ForMember(pvm => pvm.PageNumber, opts => opts.Ignore())
                .ForMember(pvm => pvm.Entries, opts => opts.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
