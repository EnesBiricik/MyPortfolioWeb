namespace MyPortfolio.Dtos
{
    public class BlogCreateDto : IDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? CoverPhoto { get; set; }
        public bool IsFeatured { get; set; }
    }
}
