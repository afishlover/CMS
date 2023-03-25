using Api.Models.DTOs;
using AutoMapper;
using CoreLayer.Entities;

namespace Api.MapperProfiles
{
    public class RootCategoryProfile : Profile
    {
        public RootCategoryProfile()
        {
            CreateMap<Category, RootCategoryDTO>();
        }
    }
}
