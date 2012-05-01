using System.Collections.Generic;

namespace Byte.Blog.Rendering.Models
{
    public class EntryCollectionViewModel
    {
        public int PageNumber { get; set; }
        public IEnumerable<EntryViewModel> Entries { get; set; }

        public EntryCollectionViewModel()
        {
            this.Entries = new List<EntryViewModel>();
        }
    }
}
