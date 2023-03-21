using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class BaseUserProfile : Profile
    {
        public BaseUserProfile()
        {
            CreateMap<BaseUser, BaseUserDTO>()
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.LastName} {src.MiddleName} {src.FirstName}"))
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday));

        }
    }
}