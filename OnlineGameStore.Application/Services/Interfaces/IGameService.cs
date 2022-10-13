﻿using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IGameService : IService<Game>
    {
        Task<IEnumerable<GameView>> GetAllAsync();

        Task<IEnumerable<GameView>> GetByGenre(Guid genreId);

        Task<IEnumerable<GameView>> GetByPlatform(Guid platformId);

        Task<GameView> GetByIdAsync(Guid gameId);

        Task<GameView> AddAsync(GameRequest gameRequest);

        Task DeleteByIdAsync(Guid gameId);

        Task UpdateAsync(Guid gameId, GameRequest gameRequest);

        Task UpdateGenresAsync(Guid gameId, List<Guid> genresIds);
    }
}