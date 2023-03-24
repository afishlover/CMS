using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles;

public class TeacherCourseProfile : Profile
{
    public TeacherCourseProfile()
    {
        CreateMap<Course, TeacherCourseDTO>()
            .ForMember(c => c.CourseCode, m => m.MapFrom(s => s.CourseCode))
            .ForMember(c => c.CategoryId, m => m.MapFrom(s => s.CategoryId))
            .ForMember(c => c.CourseName, m => m.MapFrom(s => s.CourseName))
            .ForMember(c => c.CreatorCode, m => m.MapFrom(s => s.CreatorCode))
            .ForMember(c => c.LastUpdate, m => m.MapFrom(s => s.LastUpdate != null ? s.LastUpdate.Value.ToString("dd/MM/yyyy") : string.Empty))
            .ForMember(c => c.StartDate, m => m.MapFrom(s => s.StartDate != null ? s.StartDate.Value.ToString("dd/MM/yyyy") : string.Empty))
            .ForMember(c => c.EndDate, m => m.MapFrom(s => s.EndDate != null ? s.EndDate.Value.ToString("dd/MM/yyyy") : string.Empty))
            .ForMember(c => c.Since, m => m.MapFrom(s => s.Since != null ? s.Since.Value.ToString("dd/MM/yyyy") : string.Empty));


    }
}