namespace MyPortfolio.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
