using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;
using System.Security.Principal;

namespace Api.MapperProfiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<Resource, ResourceDTO>()
                .ForMember(rd => rd.Status, m => m.MapFrom(r => Enum.GetName(r.Status)))
                .ForMember(rd => rd.ResourceType, m => m.MapFrom(r => Enum.GetName(r.ResourceType)));
        }
    }
}
