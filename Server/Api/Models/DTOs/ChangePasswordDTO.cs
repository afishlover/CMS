using Microsoft.Build.Framework;

namespace Api.Models.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        private string OldPassord { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
