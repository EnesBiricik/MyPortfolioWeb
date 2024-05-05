namespace MyPortfolio.Entities.Concrete
{
    public class Reply : BaseEntity
    {
        public string AuthorName { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public string AuthorEmailAddress { get; set; }
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
        public bool IsShared { get; set; }
    }
}
