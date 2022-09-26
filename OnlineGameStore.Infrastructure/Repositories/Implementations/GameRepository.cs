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
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        private readonly GamesContext _gamesContext;

        public GameRepository(GamesContext gamesContext) : base(gamesContext)
        {
            _gamesContext = gamesContext;
        }

        public async Task<Game> GetGameByKeyWithDetails(Guid gameKey)
        {
            return await _gamesContext.Games
                .Include(g => g.Genres).Include(g => g.Platforms).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesWithDetails()
        {
            return await _gamesContext.Games
                .Include(g => g.Genres).Include(g => g.Platforms).ToListAsync();
        }
    }
}
