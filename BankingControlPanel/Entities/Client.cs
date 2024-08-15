using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Entities
{
    public class Client
    {
        [Key]
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
        [StringLength(11)]
        public string PersonalId { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string Sex { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
