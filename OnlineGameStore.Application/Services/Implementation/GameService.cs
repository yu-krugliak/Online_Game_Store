using MapsterMapper;
using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class GameService : ServiceBase<Game>, IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper) : base(gameRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameView>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task<IEnumerable<GameView>> GetByGenre(Guid genreId)
        {
            var games = await _gameRepository.GetGamesByGenre(genreId);

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task<IEnumerable<GameView>> GetByPlatform(Guid platformId)
        {
            var games = await _gameRepository.GetGamesByPlatform(platformId);

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task<GameView> GetByIdAsync(Guid gameId)
        {
            var game = await GetExistingEntityById(gameId);

            var gameView = _mapper.Map<GameView>(game);
            return gameView;
        }

        public async Task<GameView> AddAsync(GameRequest gameRequest)
        {
            var game = _mapper.Map<Game>(gameRequest);
            game.Key = Guid.NewGuid();

            var addedGame = await _gameRepository.AddAsync(game);

            return _mapper.Map<GameView>(addedGame);
        }

        public async Task DeleteByKeyAsync(Guid gameId)
        {
            var isDeleted = await _gameRepository.DeleteByIdAsync(gameId);

            if (!isDeleted)
            {
                throw new NotFoundException("This game doesn't exist.");
            }
        }

        public async Task UpdateAsync(Guid gameId, GameRequest gameRequest)
        {
            var game = await GetExistingEntityById(gameId);
            _mapper.Map(gameRequest, game);

            var isUpdated = await _gameRepository.UpdateAsync(game);

            if (!isUpdated)
            {
                throw new ServerErrorException("Can't update this game.", null);
            }
        }
    }
}
