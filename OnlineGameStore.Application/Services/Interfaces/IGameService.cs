using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IGameService : IService<Game>
    {
        Task<IEnumerable<GameView>> GetAllAsync();

        Task<IEnumerable<GameView>> GetByGenre(int genreId);

        Task<GameView> GetByIdAsync(int gameId);

        Task<GameView> AddAsync(GameRequest gameRequest);

        Task DeleteByIdAsync(int gameId);

        Task UpdateAsync(int gameId, GameRequest gameRequest);

        Task UpdateGenresAsync(int gameId, List<int> genresIds);
    }
}