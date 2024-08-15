using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Models.AccountModels
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Admin or User
    }
}
