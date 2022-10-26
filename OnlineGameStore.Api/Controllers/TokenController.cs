using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenView), 200)]
        public async Task<IActionResult> Get([FromBody] TokenRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _tokenService.GetTokenAsync(request, cancellationToken));
        }
    }
}