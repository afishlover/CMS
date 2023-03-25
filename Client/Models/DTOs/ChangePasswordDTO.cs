using System.ComponentModel.DataAnnotations;

namespace Client.Models.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPasswordConfirmation { get; set; }

    }
}
