using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class CreateResourceProfile : Profile
    {
        public CreateResourceProfile()
        {

            CreateMap<CreateResourceDTO, Resource>()
                .ForMember(c => c.OpenTime, m => m.MapFrom(cc => DateTime.Parse(cc.OpenTime)))
                .ForMember(c => c.CloseTime, m => m.MapFrom(cc => DateTime.Parse(cc.CloseTime))); ;
		}
    }
}
