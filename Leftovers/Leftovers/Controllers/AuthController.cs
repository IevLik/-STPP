using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Leftovers.Data.Repositories;
using Leftovers.Data.Entities;
using Leftovers.Data.Dtos.Chains;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Leftovers.Data.Dtos.Auth;
using Leftovers.Auth;
using Leftovers.Auth.Model;

namespace Leftovers.Controllers
{
    [ApiController]
    [AllowAnonymous]// atributas nurodo, kad si metoda gali naudoti tik neprisijunges useris
    //[Authorize]  si metoda gali naudoti tik prisijunges useris
    //[Microsoft.AspNetCore.Components.Route(template:"api")]
    [Route( "api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<LeftoversUser> _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;
        public AuthController(UserManager<LeftoversUser> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }
        [HttpPost]
        [Route("register")] // 
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);
            if (user != null)
                return BadRequest("User already exists");
            var newUser = new LeftoversUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName
            };
            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
                return BadRequest("Could not create a user");

            await _userManager.AddToRoleAsync(newUser, LeftoversUserRoles.RestaurantUser/*SimpleUser*/);// is simpleuser pakeiciau i restaurant kolkas
            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest("User name or password is invalid");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest("User name or password is invalid");

            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);

            return Ok(new SuccessfulLoginResponseDto(accessToken));
        }
    }
}
