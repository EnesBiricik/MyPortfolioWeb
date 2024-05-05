using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models
{
    public class ReplyCreateModel
    {
        [Required(ErrorMessage = "Author Name is required")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Comment Text is required")]
        public string CommentText { get; set; }
        [Required(ErrorMessage = "Author Email Address is required"), DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Please Enter Your Email Address. The Email Address Should Not Contain '@'")]
        public string AuthorEmailAddress { get; set; }
        [Required]
        public int CommentId { get; set; }
    }
}
