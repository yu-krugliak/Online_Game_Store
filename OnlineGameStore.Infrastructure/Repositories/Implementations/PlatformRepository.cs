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
    public class PlatformRepository : RepositoryBase<PlatformType>, IPlatformRepository
    {

        private readonly GamesContext _gamesContext;

        public PlatformRepository(GamesContext gamesContext) : base(gamesContext)
        {
            _gamesContext = gamesContext;
        }

        public async Task<PlatformType> GetPlatformByIdWithDetails(Guid platformId)
        {
            return await _gamesContext.PlatformTypes
                .Include(g => g.Games).FirstOrDefaultAsync();
        }
    }
}
