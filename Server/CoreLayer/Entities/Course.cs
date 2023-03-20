namespace CoreLayer.Entities {
    public class Course {
        public Guid CourseId { get; set; }
        public string TeacherCode { get; set; }
        public Teacher Teacher { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SinceDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CreatorId { get; set; }
    }
}