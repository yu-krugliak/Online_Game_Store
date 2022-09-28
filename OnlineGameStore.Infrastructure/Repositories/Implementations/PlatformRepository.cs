using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Infrastructure.Repositories.Implementations
{
    public class PlatformRepository : RepositoryBase<PlatformType>, IPlatformRepository
    {
        public PlatformRepository(GamesContext gamesContext) : base(gamesContext) { }     
    }
}
