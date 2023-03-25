namespace Client2.Models;

public class ChangePasswordDTO
{
    public string? Username { get; set; }
    public string Email { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ReenterNewPassword { get; set; }

    public string ToString()
    {
        return Email + " " + Username + " " + OldPassword + " " + NewPassword + " " + ReenterNewPassword;
    }
}