using AutoMapper;
using CVBuilder.Identity.DTOs;
using CVBuilder.Models.Entities;

namespace CVBuilder.Identity.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<IdentityUser, IdentityUserDto>();
        CreateMap<IdentityUserDto, IdentityUser>();
    }
    
}