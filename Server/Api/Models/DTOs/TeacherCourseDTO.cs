namespace Api.Models.DTOs;

public class TeacherCourseDTO
{
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Since { get; set; }
    public DateTime? LastUpdate { get; set; }
    public Guid CategoryId { get; set; }
    public string? CreatorCode { get; set; }
}