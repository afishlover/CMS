using CoreLayer.Enums;

namespace CoreLayer.Entities {
    public class Teacher {
        public Guid TeacherId { get; set; }
        public string TeacherCode { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid BaseUserId { get; set; }
        public BaseUser BaseUser { get; set; }
    }
}