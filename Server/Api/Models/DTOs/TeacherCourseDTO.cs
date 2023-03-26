namespace Api.Models.DTOs;

public class TeacherCourseDTO
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Since { get; set; }
    public string? LastUpdate { get; set; }
    public Guid CategoryId { get; set; }
    public string? CreatorCode { get; set; }
}