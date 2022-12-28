using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("gettoken")]
        public async Task<IActionResult> Get([FromBody] TokenRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _tokenService.GetTokenAsync(request, cancellationToken));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
        {
            return Ok(await _tokenService.RefreshToken(refreshTokenRequest, cancellationToken));
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            return Ok(await _tokenService.RevokeTokenAsync());
        }
    }
}