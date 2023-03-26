using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class CreateResourceProfile : Profile
    {
        public CreateResourceProfile()
        {

            CreateMap<Resource, CreateResourceDTO>();
        }
    }
}
