using AutoMapper;
using EatHealthyApi.Dtos.ReferentialDataDtos;
using EatHealthyApi.Models;

namespace EatHealthyApi.Profiles.ReferentialData
{
    public class FoodPreferencesProfile : Profile
    {
        public FoodPreferencesProfile()
        {
            CreateMap<FoodPreferences, FoodPreferencesReadDto>();
            CreateMap<FoodPreferencesCreateDto, FoodPreferences>();
        }
    }
}