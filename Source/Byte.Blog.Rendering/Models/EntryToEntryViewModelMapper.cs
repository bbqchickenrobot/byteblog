using AutoMapper;
using Byte.Blog.Content;
using MarkdownSharp;

namespace Byte.Blog.Rendering.Models
{
    public class EntryToEntryViewModelMapper
    {
        public EntryToEntryViewModelMapper()
        {
        }

        public EntryViewModel Map(Entry entry)
        {
            var entryViewModel = Mapper.Map<EntryViewModel>(entry);

            var markdown = new Markdown();

            entryViewModel.Body = markdown.Transform(entryViewModel.Body);

            return entryViewModel;
        }
    }
}
