using AutoMapper;
using EatHealthyApi.Dtos.ReferentialDataDtos;
using EatHealthyApi.Models;

namespace EatHealthyApi.Profiles.ReferentialData
{
    public class DislikeProfile : Profile
    {
        public DislikeProfile()
        {
            CreateMap<DislikeItems, DislikeReadDto>();
            CreateMap<DislikeCreateDto, DislikeItems>();
        }
    }
}