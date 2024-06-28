using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models
{
    public class ContactCreateModel
    {
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "İsim 50 karakterden uzun olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi formatı.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Konu alanı zorunludur.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj alanı zorunludur.")]
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;

        public string? reCaptcha { get; set; } 
    }
}
