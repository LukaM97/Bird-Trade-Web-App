﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBirdApp.Repository.Interfaces;
using NewBirdApp.Models;
using NewBirdApp.Models.DTO;
using System.Threading.Tasks;

namespace NewBirdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO regDTO)
        {
            if (await UserExistsUsername(regDTO.Username)) return BadRequest("Username already exists.");
            if (await UserExistsEmail(regDTO.Email)) return BadRequest("Account with that Email already exists.");

            var user = new AppUser
            {
                UserName = regDTO.Username,
                Email = regDTO.Email,
                DateOfBirth = regDTO.DateOfBirth,
                Name = regDTO.Name,
                Surname = regDTO.Surname,
                City = regDTO.City,
                Country = regDTO.Country
            };

            var result = await _userManager.CreateAsync(user, regDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Name = user.Name,
                Surname = user.Surname,
                LastOnline = user.LastOnline
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username);

            if (user == null) return Unauthorized("Invalid username");

            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!result) return Unauthorized();

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Name = user.Name,
                Surname = user.Surname,
                LastOnline = user.LastOnline
            };
        }

        private async Task<bool> UserExistsUsername(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username);
        }

        private async Task<bool> UserExistsEmail(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);
        }
    }
}
