using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models
{
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "Username cannot be empty!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Email cannot be empty!")]
        [DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Please check your Email address. Email Address must contains '@' ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password cannot be empty!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

    }
}
