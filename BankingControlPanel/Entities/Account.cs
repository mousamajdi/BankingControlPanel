using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
