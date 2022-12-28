using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByGameIdAsync(int gameId);
    }
}
