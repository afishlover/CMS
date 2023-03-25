namespace Client.Models.DTOs;

public class UserDTO
{
    public Guid AccountId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Birthdate { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
    public string Phone { get; set; }
}