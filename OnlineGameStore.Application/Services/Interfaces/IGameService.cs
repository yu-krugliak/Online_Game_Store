using Microsoft.AspNetCore.Http;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IGameService : IService<Game>
    {
        Task<IEnumerable<GameView>> GetAllAsync();

        Task<IEnumerable<GameView>> GetByGenresAndNameAsync(List<int> genresIds, string? name);

        Task<GameView> GetByIdAsync(int gameId);

        Task<GameView> AddAsync(GameRequest gameRequest);

        Task DeleteByIdAsync(int gameId);

        Task UpdateAsync(int gameId, GameRequest gameRequest);

        Task UpdateImageAsync(int gameId, IFormFile image);
    }
}