using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Identity;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Infrastructure.Repositories.Implementations;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly GamesContext _gameContext;

    public UserRepository(GamesContext gameContext) : base(gameContext)
    {
        _gameContext = gameContext;
    }
}