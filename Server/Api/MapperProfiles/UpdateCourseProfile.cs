using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class UpdateCourseProfile : Profile
    {
        public UpdateCourseProfile()
        {
            CreateMap<UpdateCourseDTO, Course>();
        }
    }
}
