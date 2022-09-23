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
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(GamesContext gamesContext) : base(gamesContext)
        {
        }
    }
}
