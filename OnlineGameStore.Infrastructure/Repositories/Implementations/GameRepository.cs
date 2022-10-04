﻿using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Infrastructure.Repositories.Implementations
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        private readonly GamesContext _gamesContext;

        public GameRepository(GamesContext gamesContext) : base(gamesContext)
        {
            _gamesContext = gamesContext;
        }

        public async Task<Game?> GetGameByIdWithDetails(Guid gameId)
        {
            return await _gamesContext.Games
                .Where(game => game.Id == gameId)
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .FirstOrDefaultAsync();
        }

        public async Task<Game?> GetGameByKeyWithDetails(string gameKey)
        {
            return await _gamesContext.Games
                .Where(game => game.Key == gameKey)
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByGenre(Guid genreId)
        {
            return await _gamesContext.Games
                .Include(game => game.Genres)
                .Where(game =>
                    game.Genres!.Any(genre => genre.Id == genreId)
                )
                .Include(game => game.Platforms)
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByPlatform(Guid platformId)
        {
            return await _gamesContext.Games
                .Include(game => game.Platforms)
                .Where(game => 
                    game.Platforms!.Any(platform => platform.Id == platformId)
                )
                .Include(game => game.Genres)
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesWithDetails()
        {
            return await _gamesContext.Games
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .ToListAsync();
        }
    }
}
