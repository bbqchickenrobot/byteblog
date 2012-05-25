using System.Collections.Generic;

namespace Byte.Blog.Rendering.Models
{
    public class SidebarViewModel
    {
        public IEnumerable<WidgetViewModel> Widgets { get; set; }

        public SidebarViewModel()
        {
            this.Widgets = new List<WidgetViewModel>();
        }
    }
}