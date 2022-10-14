using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetGamesWithDetails();

        Task<Game?> GetGameByIdWithDetails(int gameId);

        Task<Game?> GetGameByKeyWithDetails(string gameKey);

        Task<IEnumerable<Game>> GetGamesByGenre(int genreId);

        Task<bool> RemoveGenresFromGame(Game game);

        Task<bool> RemovePlatformsFromGame(Game game);
    }
}
