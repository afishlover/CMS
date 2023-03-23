namespace Api.Models.DTOs;

public class UserDTO
{
    public Guid AccountId { get; set; }
    public string Email { get; set; }
    public Guid UserId { get; set; }
}