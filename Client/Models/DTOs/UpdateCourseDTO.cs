namespace Client.Models.DTOs
{
    public class UpdateCourseDTO
    {
		public Guid CourseId { get; set; }
		public string CourseName { get; set; }
		public string CourseCode { get; set; }
	}
}
