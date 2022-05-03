using AutoMapper;
using EatHealthyApi.Dtos.ReferentialDataDtos;
using EatHealthyApi.Models;

namespace EatHealthyApi.Profiles.ReferentialData
{
    public class ActivityTypeProfile : Profile
    {
        public ActivityTypeProfile()
        {
            CreateMap<ActivityType, ActivityTypeReadDto>();
            CreateMap<ActivityTypeCreateDto, ActivityType>();
        }
    }
}