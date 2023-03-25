namespace Client.Models.DTOs;

public class StudentCourseDTO
{
    public string TeacherCode { get; set; }
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public string EnrollDate { get; set; }
    public Guid CategoryId { get; set; }
}
/*
 *         public Guid CourseId { get; set; }
        public string TeacherCode { get; set; }
        public Teacher Teacher { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SinceDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string CreatorId { get; set; }
*/