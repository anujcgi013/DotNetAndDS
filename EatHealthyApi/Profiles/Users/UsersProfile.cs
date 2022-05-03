using AutoMapper;
using EatHealthyApi.Dtos.UserDtos;
using EatHealthyApi.Models;

namespace EatHealthyApi.Profiles.Users
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<ApplicationUser, UserReadDto>();
            CreateMap<UserCreateDto, ApplicationUser>();
            //CreateMap<UseLoginDto, ApplicationUser>();
        }
    }
}