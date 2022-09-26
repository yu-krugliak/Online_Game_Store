using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;
using System.Net;

namespace OnlineGameStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            return Ok(await _gameService.GetAllAsync());
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddGame([FromBody] GameRequest request)
        {
            var result = await _gameService.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateGame(Guid gameKey, [FromBody] GameRequest request)
        {
            await _gameService.UpdateAsync(gameKey, request);
            return Ok();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteGame(Guid gameKey)
        {
            await _gameService.DeleteByKeyAsync(gameKey);
            return Ok();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetGameById(Guid gameKey)
        {
            return Ok(await _gameService.GetByIdAsync(gameKey));
        }

        [HttpGet("bygenre")]
        public async Task<IActionResult> GetAllGamesByGenre(Guid genreId)
        {
            return Ok(await _gameService.GetByGenre(genreId));
        }

        [HttpGet("byplatform")]
        public async Task<IActionResult> GetAllGamesByPlatform(Guid platformId)
        {
            return Ok(await _gameService.GetByPlatform(platformId));
        }
        
    }
}