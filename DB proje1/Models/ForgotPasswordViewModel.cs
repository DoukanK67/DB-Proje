using System.ComponentModel.DataAnnotations;

namespace DB_proje1.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
