using OnlineGameStore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Infrastructure.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetGamesWithDetails();

        Task<Game> GetGameByKeyWithDetails(Guid gameKey);
    }
}
