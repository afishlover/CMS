using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class DetailedUserProfile : Profile
    {
        public DetailedUserProfile()
        {
            CreateMap<(Student, User), DetailedUserDTO>()
                .ForMember(dud => dud.StudentCode, m => m.MapFrom(s => s.Item1.StudentCode))
                .ForMember(dud => dud.Address, m => m.MapFrom(s => s.Item2.Address))
                .ForMember(dud => dud.FullName, m => m.MapFrom(s => s.Item2.GetFullName()))
                .ForMember(dud => dud.Phone, m => m.MapFrom(s => s.Item2.Phone))
                .ForMember(dud => dud.Major, m => m.MapFrom(s => Enum.GetName(s.Item1.Major) ?? string.Empty))
                .ForMember(dud => dud.Gender, m => m.MapFrom(s => Enum.GetName(s.Item2.Gender) ?? string.Empty))
                .ForMember(dud => dud.Birthday, m => m.MapFrom(s => s.Item2.Birthday != null ? s.Item2.Birthday.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(dud => dud.ProfilePicture, m => m.MapFrom(s => s.Item2.ProfilePicture))
                .ForMember(dud => dud.CoverPicture, m => m.MapFrom(s => s.Item2.CoverPicture));

            CreateMap<(Teacher, User), DetailedUserDTO>()
                .ForMember(dud => dud.TeacherCode, m => m.MapFrom(s => s.Item1.TeacherCode))
                .ForMember(dud => dud.Address, m => m.MapFrom(s => s.Item2.Address))
                .ForMember(dud => dud.FullName, m => m.MapFrom(s => s.Item2.GetFullName()))
                .ForMember(dud => dud.Phone, m => m.MapFrom(s => s.Item2.Phone))
                .ForMember(dud => dud.Department, m => m.MapFrom(s => Enum.GetName(s.Item1.Department) ?? string.Empty))
                .ForMember(dud => dud.Gender, m => m.MapFrom(s => Enum.GetName(s.Item2.Gender) ?? string.Empty))
                .ForMember(dud => dud.Birthday, m => m.MapFrom(s => s.Item2.Birthday != null ? s.Item2.Birthday.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(dud => dud.ProfilePicture, m => m.MapFrom(s => s.Item2.ProfilePicture))
                .ForMember(dud => dud.CoverPicture, m => m.MapFrom(s => s.Item2.CoverPicture));
        }
    }
}
