using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameView>> GetAllAsync();
        //Task<IEnumerable<Game>> GetAllWithDetailsAsync();

        Task<IEnumerable<GameView>> GetByGenre(Guid genreId);
        Task<IEnumerable<GameView>> GetByPlatform(Guid platformId);

        Task<GameView> GetByIdAsync(Guid gameKey);
        //Task<Game> GetByIdWithDetailsAsync(Guid gameKey);

        Task<GameView> AddAsync(GameRequest gameRequest);

        Task DeleteByKeyAsync(Guid gameKey);

        Task UpdateAsync(Guid gameKey, GameRequest gameRequest);
    }
}
