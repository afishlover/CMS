using CoreLayer.Enums;

namespace CoreLayer.Entities
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
    }
}