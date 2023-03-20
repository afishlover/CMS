using System.ComponentModel.DataAnnotations;

namespace Client.Models.DTOs
{
    public class AccountDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}