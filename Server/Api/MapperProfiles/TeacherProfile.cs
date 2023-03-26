using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<(Teacher, User), TeacherDTO>()
                .ForMember(td => td.TeacherCode, m => m.MapFrom(s => s.Item1.TeacherCode))
                .ForMember(td => td.Address, m => m.MapFrom(s => s.Item2.Address))
                .ForMember(td => td.FullName, m => m.MapFrom(s => s.Item2.GetFullName()))
                .ForMember(td => td.Phone, m => m.MapFrom(s => s.Item2.Phone))
                .ForMember(td => td.Department, m => m.MapFrom(s => Enum.GetName(s.Item1.Department) ?? string.Empty))
                .ForMember(td => td.Gender, m => m.MapFrom(s => Enum.GetName(s.Item2.Gender) ?? string.Empty))
                .ForMember(td => td.Birthday, m => m.MapFrom(s => s.Item2.Birthday != null ? s.Item2.Birthday.Value.ToString("dd/MM/yyyy") : string.Empty));
        }
    }
}
