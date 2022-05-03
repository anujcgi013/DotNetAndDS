using AutoMapper;
using EatHealthyApi.Dtos.ReferentialDataDtos;
using EatHealthyApi.Models;

namespace EatHealthyApi.Profiles.ReferentialData
{
    public class VarietyTypeProfile : Profile
    {
        public VarietyTypeProfile()
        {
            CreateMap<VarietyType, VarietyTypeReadDto>();
            CreateMap<VarietyTypeCreateDto, VarietyType>();
        }
    }
}