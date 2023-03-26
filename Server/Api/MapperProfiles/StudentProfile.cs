using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<(StudentCourse, Student, User), StudentDTO>()
                .ForMember(sd => sd.StudentCode, m => m.MapFrom(s => s.Item2.StudentCode))
                .ForMember(sd => sd.Address, m => m.MapFrom(s => s.Item3.Address))
                .ForMember(sd => sd.FullName, m => m.MapFrom(s => s.Item3.GetFullName()))
                .ForMember(sd => sd.Phone, m => m.MapFrom(s => s.Item3.Phone))
                .ForMember(sd => sd.Major, m => m.MapFrom(s => Enum.GetName(s.Item2.Major) ?? string.Empty))
                .ForMember(sd => sd.Gender, m => m.MapFrom(s => Enum.GetName(s.Item3.Gender) ?? string.Empty))
                .ForMember(sd => sd.Birthday, m => m.MapFrom(s => s.Item3.Birthday != null?s.Item3.Birthday.Value.ToString("dd/MM/yyyy") : string.Empty));
        }
    }
}
