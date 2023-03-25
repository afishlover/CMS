using Client2.Models.Enum;

namespace Client2.Models;

public class AccountDTO
{
    public Guid AccountId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
    public Role Role { get; set; }
}