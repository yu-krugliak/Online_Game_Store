using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetGamesWithDetails();

        Task<Game?> GetGameByKeyWithDetails(Guid gameKey);

        Task<IEnumerable<Game>> GetGamesByGenre(Guid genreId);

        Task<IEnumerable<Game>> GetGamesByPlatform(Guid platformId);
    }
}
