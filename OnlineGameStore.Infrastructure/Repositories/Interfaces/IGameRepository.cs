using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetGamesWithDetailsAsync();

        Task<Game?> GetGameByIdWithDetailsAsync(int gameId);

        Task<Game?> GetGameByKeyWithDetailsAsync(string gameKey);

        Task<IEnumerable<Game>> FilterGamesByGenresAndNameAsync(List<int> genresIds, string? name);

        bool UpdateGameImage(Game game);

        Task<bool> RemoveGenresFromGameAsync(Game game);

        Task<bool> RemovePlatformsFromGameAsync(Game game);
    }
}
