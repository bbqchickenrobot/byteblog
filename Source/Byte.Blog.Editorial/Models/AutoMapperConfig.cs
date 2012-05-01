using AutoMapper;
using Byte.Blog.Content;

namespace Byte.Blog.Editorial.Models
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Entry, EntryEditModel>()
                .ForMember(editModel => editModel.Preview, opts => opts.Ignore())
                .ForMember(editModel => editModel.PageTitle, opts => opts.Ignore())
                .ForMember(editModel => editModel.PageColor, opts => opts.Ignore())
                .ForMember(editModel => editModel.PossiblePages, opts => opts.Ignore());

            Mapper.CreateMap<EntryEditModel, Entry>();

            Mapper.CreateMap<Page, PageEditModel>()
                .ForMember(editModel => editModel.PossibleHtmlColors, opts => opts.Ignore());

            Mapper.CreateMap<PageEditModel, Page>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
