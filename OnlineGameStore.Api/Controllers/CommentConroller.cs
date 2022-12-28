using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("getbygame/{gameId}")]
        public async Task<IActionResult> GetAllCommentsByGame(int gameId)
        {
            return Ok(await _commentService.GetByGameAsync(gameId));
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _commentService.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest request)
        {
            var result = await _commentService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CommentRequest request)
        {
            await _commentService.UpdateAsync(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}