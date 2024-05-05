using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.Dtos
{
    public class ReplyListDto : IDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public string AuthorEmailAddress { get; set; }
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
        public bool IsShared { get; set; }

    }
}
