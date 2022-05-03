using AutoMapper;
using EatHealthyApi.Dtos.ReferentialDataDtos;
using EatHealthyApi.Models;

namespace EatHealthyApi.Profiles.ReferentialData
{
    public class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<Goals, GoalReadDto>();
            CreateMap<GoalCreateDto, Goals>();
        }
    }
}