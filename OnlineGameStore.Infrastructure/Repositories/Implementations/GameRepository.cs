using Microsoft.EntityFrameworkCore;
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

        public async Task<Game?> GetGameByIdWithDetailsAsync(int gameId)
        {
            return await _gamesContext.Games
                .Where(game => game.Id == gameId)
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Game?> GetGameByKeyWithDetailsAsync(string gameKey)
        {
            return await _gamesContext.Games
                .Where(game => game.Key == gameKey)
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Game>> FilterGamesByGenresAndNameAsync(List<int> genresIds, string? name)
        {
            var filtered = _gamesContext.Games
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .AsNoTracking();

            if (genresIds.Any())
            {
                filtered = filtered.Where(game =>
                    game.Genres!.Any(genre => genresIds.Contains(genre.Id)));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                filtered = filtered.Where(game => game.Name!.Contains(name));
            }

            return await filtered.ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesWithDetailsAsync()
        {
            return await _gamesContext.Games
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool UpdateGameImage(Game game)
        {
            _gamesContext.Games
                .Where(g => g.Id == game.Id)
                .ExecuteUpdate(g => g.SetProperty(u => u.ImageUrl, game.ImageUrl));

            return true;
        }

        public async Task<bool> RemoveGenresFromGameAsync(Game game)
        {
            game.Genres.Clear();
            await _gamesContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemovePlatformsFromGameAsync(Game game)
        {
            game.Platforms.Clear();
            await _gamesContext.SaveChangesAsync();

            return true;
        }
    }
}
