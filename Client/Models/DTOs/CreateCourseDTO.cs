namespace Client.Models.DTOs
{
    public class CreateCourseDTO
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
