using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IGenreService
    {
        Task<GenreView> AddAsync(GenreRequest genreRequest);

    }
}
