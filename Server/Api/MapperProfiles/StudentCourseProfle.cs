using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles;

public class StudentCourseProfle : Profile
{
    public StudentCourseProfle()
    {
        CreateMap<(Course, Teacher, StudentCourse), StudentCourseDTO>()
            .ForMember(scd => scd.CategoryId, m => m.MapFrom(s => s.Item1.CategoryId))
            .ForMember(scd => scd.CourseCode, m => m.MapFrom(s => s.Item1.CourseCode))
            .ForMember(scd => scd.CourseName, m => m.MapFrom(s => s.Item1.CourseName))
            .ForMember(scd => scd.EnrollDate,
                m => m.MapFrom(s =>
                    s.Item3.EnrollDate != null ? s.Item3.EnrollDate.Value.ToString("dd/MM/yyyy") : string.Empty))
            .ForMember(scd => scd.TeacherCode, m => m.MapFrom(s => s.Item2.TeacherCode));
    }
}