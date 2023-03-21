namespace Api.Models.DTOs;

public class BaseUserDTO
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public DateTime Birthday { get; set; }
    public string Phone { get; set; }
}