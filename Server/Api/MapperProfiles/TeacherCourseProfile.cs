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
            .ForMember(c => c.LastUpdate, m => m.MapFrom(s => s.LastUpdate))
            .ForMember(c => c.StartDate, m => m.MapFrom(s => s.StartDate))
            .ForMember(c => c.EndDate, m => m.MapFrom(s => s.EndDate))
            .ForMember(c => c.Since, m => m.MapFrom(s => s.Since));


    }
}