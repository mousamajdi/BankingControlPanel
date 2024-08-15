using BankingControlPanel.Enums;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerSchema("User role (e.g., 'Admin', 'User')")]
        public UserRole Role { get; set; } // Admin or User
    }
}
