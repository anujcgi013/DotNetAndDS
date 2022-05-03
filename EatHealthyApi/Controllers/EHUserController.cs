using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using EatHealthyApi.Data.Users;
using EatHealthyApi.Dtos.UserDtos;
using EatHealthyApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EatHealthyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EHUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;

        public EHUserController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> Register(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<ApplicationUser>(userCreateDto);
            userModel.CreatedDate = DateTime.Now.ToString("dd-MMM-yyyy");
            userModel.isDeleted = false;
            userModel.UserName = userModel.Email;
            try
            {
                var result = await _userManager.CreateAsync(userModel, userCreateDto.password);
                if (result.Succeeded)
                {
                    dynamic details = new ExpandoObject();
                    details.User_Id = userModel.Id;
                    details.succeeded = true;
                    return Ok(details);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<Object> Login(UseLoginDto userLoginDto)
        {
            try
            {
                var result = await _singInManager.PasswordSignInAsync(userLoginDto.Email,
                userLoginDto.password, false, false);
                if (result.Succeeded)
                {
                    dynamic details = new ExpandoObject();
                    details.User_Id = _userManager.GetUserId(HttpContext.User);
                    details.succeeded = true;
                    return Ok(details);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("Logout")]
        public async Task<Object> Logout()
        {
            await _singInManager.SignOutAsync();
            return Ok("Logged Out Successfully");
        }
    }
}