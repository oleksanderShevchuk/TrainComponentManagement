using AutoMapper;
using TrainComponentApi.DTOs;
using TrainComponentApi.Models;

namespace TrainComponentApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Component, ComponentDto>().ReverseMap();
            CreateMap<CreateComponentDto, Component>();
        }
    }
}
