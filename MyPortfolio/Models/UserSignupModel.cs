using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models
{
    public class UserSignupModel
    {


        [Required(ErrorMessage = "Username cannot be empty!")]
        public string UserName { get; set; }



        [Required(ErrorMessage = "Email cannot be empty!")]
        [DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Please check your Email address. Email Address must contains '@' ")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password cannot be empty!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Password cannot be empty!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are don't match!")]
        public string ConfirmPassword { get; set; }
    }
}
