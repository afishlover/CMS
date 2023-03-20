namespace CoreLayer.Entities {
    public class Student {
        public Guid StudentId { get; set; }
        public string StudentCode { get; set; }
        public Guid BaseUserId { get; set; }
        public BaseUser BaseUser { get; set; }
    }
}