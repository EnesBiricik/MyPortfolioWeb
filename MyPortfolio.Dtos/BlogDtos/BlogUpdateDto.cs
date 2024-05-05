namespace MyPortfolio.Dtos
{
    public class BlogUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? CoverPhoto { get; set; }
        public DateTime Date { get; set; }
        public bool IsFeatured { get; set; }
    }
}
