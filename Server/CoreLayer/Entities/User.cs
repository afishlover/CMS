using CoreLayer.Enums;

namespace CoreLayer.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string CoverPicture { get; set; }
        public string ProfilePicture { get; set; }
        public Guid AccountId { get; set; }
        public string GetFullName() {
            return $"{LastName} {MiddleName} {FirstName}";
        }
    }
}