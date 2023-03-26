using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class CreateCourseProfile : Profile
    {
        public CreateCourseProfile()
        {
            CreateMap<CreateCourseDTO, Course>();
        }
    }
}
