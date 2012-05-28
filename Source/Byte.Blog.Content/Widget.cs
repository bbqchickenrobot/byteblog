
namespace Byte.Blog.Content
{
    public abstract class Widget
    {
        public static readonly string IdPrefix = "widgets/";

        public string Id { get; set; }

        protected Widget() 
            : this(IdPrefix)
        {
        }

        protected Widget(string id) 
        {
            this.Id = id;
        }
    }
}
