using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IGenreService : IService<Genre>
    {
        Task<IEnumerable<GenreView>> GetAllAsync();

        Task<GenreView> GetByIdAsync(int genreId);

        Task<GenreView> AddAsync(GenreRequest genreRequest);
    }
}