namespace MyPortfolio.Entities.Concrete
{
    public class Blog : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string CoverPhoto { get; set; }
        public DateTime Date { get; set; }
        public ushort ReadingTime { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsFeatured { get; set; }

    }
}
