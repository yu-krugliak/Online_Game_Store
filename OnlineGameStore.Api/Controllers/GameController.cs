using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Services.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] GameRequest request)
        {
            var result = await _gameService.AddAsync(request);
            return CreatedAtAction(nameof(GetGameById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame(Guid gameId, [FromBody] GameRequest request)
        {
            await _gameService.UpdateAsync(gameId, request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGame(Guid gameId)
        {
            await _gameService.DeleteByIdAsync(gameId);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            return Ok(await _gameService.GetByIdAsync(id));
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