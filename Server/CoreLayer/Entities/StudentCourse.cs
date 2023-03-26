namespace CoreLayer.Entities {
    public class StudentCourse {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime? EnrollDate { get; set; }
    }
}