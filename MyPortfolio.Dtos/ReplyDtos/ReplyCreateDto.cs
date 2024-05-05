namespace MyPortfolio.Dtos
{
    public class ReplyCreateDto : IDto
    {
        public string AuthorName { get; set; }
        public string CommentText { get; set; }
        public string AuthorEmailAddress { get; set; }
        public int CommentId { get; set; }
        public bool IsShared { get; set; }
    }
}
