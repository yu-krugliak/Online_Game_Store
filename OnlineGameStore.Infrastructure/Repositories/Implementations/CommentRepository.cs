using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Infrastructure.Repositories.Implementations
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        private readonly GamesContext _gamesContext;

        public CommentRepository(GamesContext gamesContext) : base(gamesContext)
        {
            _gamesContext = gamesContext;
        }

        public async Task<IEnumerable<Comment>> GetByGameKey(Guid gameKey)
        {
            return await _gamesContext.Comments
                .Where(c => c.GameId == gameKey).OrderBy(c => c.DatePosted).ToListAsync();
        }
    }
}
