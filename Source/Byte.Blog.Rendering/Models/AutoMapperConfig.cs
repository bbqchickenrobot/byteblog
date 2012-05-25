using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Rendering.Controllers;

namespace Byte.Blog.Rendering.Models
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Entry, EntryViewModel>()
                .ForMember(evm => evm.PageSlug, opts => opts.Ignore())
                .ForMember(evm => evm.CanonicalUrl, opts => opts.Ignore())
                .ForMember(evm => evm.DisqusThread, opts => opts.Ignore());

            Mapper.CreateMap<Page, PageViewModel>()
                .ForMember(pvm => pvm.PageNumber, opts => opts.Ignore())
                .ForMember(pvm => pvm.DisqusShortname, opts => opts.Ignore())
                .ForMember(pvm => pvm.Entries, opts => opts.Ignore());

            Mapper.CreateMap<Widget, WidgetViewModel>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
