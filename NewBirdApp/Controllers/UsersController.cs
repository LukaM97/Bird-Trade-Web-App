using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBirdApp.Models;
using NewBirdApp.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBirdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
        {

            return Ok(await _userRepository.GetUsersAsync());
        }

        [Authorize(Roles = "Member,Admin")]
        [HttpGet("{id}")]
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}
