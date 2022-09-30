using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("newcomment")]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest request)
        {
            var result = await _commentService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetAllCommentsByGame(Guid gameId)
        {
            return Ok(await _commentService.GetByGame(gameId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _commentService.GetByIdAsync(id));
        }
    }
}