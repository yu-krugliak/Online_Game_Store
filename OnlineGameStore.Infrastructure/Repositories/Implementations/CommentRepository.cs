using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Infrastructure.Repositories.Implementations
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        private readonly GamesContext _gamesContext;

        public CommentRepository(GamesContext gamesContext) : base(gamesContext)
        {
            _gamesContext = gamesContext;
        }

        public async Task<IEnumerable<Comment>> GetByGameId(int gameId)
        {
            return await _gamesContext.Comments
                .Where(c => c.GameId == gameId).ToListAsync();
        }
    }
}
