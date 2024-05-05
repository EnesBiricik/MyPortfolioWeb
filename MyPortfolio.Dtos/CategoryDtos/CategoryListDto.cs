using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.Dtos
{
    public class CategoryListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
