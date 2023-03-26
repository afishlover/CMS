using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class CreateCourseProfile : Profile
    {
        public CreateCourseProfile()
        {
            CreateMap<CreateCourseDTO, Course>()
				.ForMember(c => c.StartDate, m => m.MapFrom(cc => DateTime.Parse(cc.StartDate)))
				.ForMember(c => c.EndDate, m => m.MapFrom(cc => DateTime.Parse(cc.EndDate)));
        }
    }
}
