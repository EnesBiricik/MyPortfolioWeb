namespace MyPortfolio.Dtos
{
    public class CommentCreateDto : IDto
    {
        public string AuthorName { get; set; }
        public string CommentText { get; set; }
        public string AuthorEmailAddress { get; set; }
        public int BlogId { get; set; }
        public bool IsShared { get; set; }

    }
}
