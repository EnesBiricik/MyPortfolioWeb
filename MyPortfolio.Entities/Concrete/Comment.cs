namespace MyPortfolio.Entities.Concrete
{
    public class Comment : BaseEntity
    {
        public string AuthorName { get; set; }
        public string CommentText { get; set; }
        public string AuthorEmailAddress { get; set; }
        public DateTime Date { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public List<Reply> Replies { get; set; }
        public bool IsShared { get; set; }
    }
}
