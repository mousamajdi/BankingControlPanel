using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.DTOs
{
    public class ClientDTO : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Personal ID must be exactly 11 characters long.")]
        public string PersonalId { get; set; }

        [Required]
        [RegularExpression(@"^\+\d{1,3}\d{10}$", ErrorMessage = "Mobile number must be in the correct format with country code.")]
        public string MobileNumber { get; set; }

        [Required]
        public string Sex { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public List<AccountDTO> Accounts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Accounts == null || Accounts.Count == 0)
            {
                yield return new ValidationResult("At least one account is required.", new[] { nameof(Accounts) });
            }
        }
    }
}
