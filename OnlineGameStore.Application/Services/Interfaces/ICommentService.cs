using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface ICommentService : IService<Comment>
    {
        Task<IEnumerable<CommentView>> GetByGame(int gameId);

        Task<CommentView> GetByIdAsync(int commentId);

        Task<CommentView> AddAsync(CommentRequest commentRequest);
    }
}