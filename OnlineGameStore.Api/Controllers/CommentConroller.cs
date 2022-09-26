using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentConroller : ControllerBase
    {

        private readonly ICommentService _commentService;

        public CommentConroller(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("newcomment")]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest request)
        {
            var result = await _commentService.AddAsync(request);
            return CreatedAtAction(nameof(Comment), new { id = result.Id }, result);
        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetAllCommentsByGame(Guid gameKey)
        {
            return Ok(await _commentService.GetByGame(gameKey));
        }
    }
}
