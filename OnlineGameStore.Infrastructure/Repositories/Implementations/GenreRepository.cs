using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Infrastructure.Repositories.Implementations
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        private readonly GamesContext _gamesContext;

        public GenreRepository(GamesContext gamesContext) : base(gamesContext)
        {
            _gamesContext = gamesContext;
        }

        public async Task<Genre> GetGenreByIdWithDetails(Guid genreId)
        {
            return await _gamesContext.Genres
                .Include(g => g.Games).FirstOrDefaultAsync();
        }
    }
}
