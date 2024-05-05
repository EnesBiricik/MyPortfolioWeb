using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.Dtos
{
    public class CommentListDto : IDto
    {
        public int Id { get; set; }
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
