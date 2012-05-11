namespace Byte.Blog.Editorial.Models
{
    public class PageDashboardQueryModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}