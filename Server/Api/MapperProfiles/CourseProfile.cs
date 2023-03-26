using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<(Course, Category), CourseDTO>()
                .ForMember(cd => cd.CourseId, m => m.MapFrom(s => s.Item1.CourseId))
                .ForMember(cd => cd.CourseCode, m => m.MapFrom(s => s.Item1.CourseCode))
                .ForMember(cd => cd.CategoryName, m => m.MapFrom(s => s.Item2.CategoryName))
                .ForMember(cd => cd.CourseName, m => m.MapFrom(s => s.Item1.CourseName))
                .ForMember(cd => cd.CreatorCode, m => m.MapFrom(s => s.Item1.CreatorCode))
                .ForMember(cd => cd.LastUpdate, m => m.MapFrom(s => s.Item1.LastUpdate != null ? s.Item1.LastUpdate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(cd => cd.StartDate, m => m.MapFrom(s => s.Item1 .StartDate != null ? s.Item1.StartDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(cd => cd.EndDate, m => m.MapFrom(s => s.Item1.EndDate != null ? s.Item1.EndDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(cd => cd.Since, m => m.MapFrom(s => s.Item1.Since != null ? s.Item1.Since.Value.ToString("dd/MM/yyyy") : string.Empty));
        }
    }
}
