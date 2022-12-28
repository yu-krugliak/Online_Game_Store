using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _userService.RegisterAsync(request);
            return Ok();
        }

        [HttpGet("getbyemail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _userService.GetUserByEmail(email));
        }

        [HttpPut("addavatar")]
        [Authorize]
        public async Task<IActionResult> UpdateImageInGame(IFormFile image)
        {
            await _userService.UpdateAvatarAsync(image);
            return Ok();
        }
    }
}