﻿using OnlineGameStore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<Genre> GetGenreByIdWithDetails(Guid genreId);
    }
}
