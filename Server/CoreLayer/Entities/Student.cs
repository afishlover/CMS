using CoreLayer.Enums;

namespace CoreLayer.Entities {
    public class Student {
        public Guid StudentId { get; set; }
        public string StudentCode { get; set; }
        public Guid UserId { get; set; }
        public Major Major { get; set; }
    }
}