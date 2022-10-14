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
        public async Task<IActionResult> UpdateGame(int gameId, [FromBody] GameRequest request)
        {
            await _gameService.UpdateAsync(gameId, request);
            return Ok();
        }

        [HttpPut("addgenres")]
        public async Task<IActionResult> UpdateGenresInGame(int gameId, [FromBody] List<int> genresIds)
        {
            await _gameService.UpdateGenresAsync(gameId, genresIds);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGame(int gameId)
        {
            await _gameService.DeleteByIdAsync(gameId);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            return Ok(await _gameService.GetByIdAsync(id));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetGameByGenresAndName(string? name, [FromQuery] List<int> genresIds)
        {
            return Ok(await _gameService.GetByGenresAndNameAsync(genresIds, name));
        }
    }
}