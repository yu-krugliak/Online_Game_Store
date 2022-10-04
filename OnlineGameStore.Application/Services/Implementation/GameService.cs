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
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformRepository _platformRepository;

        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IGenreRepository genreRepository, 
            IPlatformRepository platformRepository, IMapper mapper) : base(gameRepository)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameView>> GetAllAsync()
        {
            var games = await _gameRepository.GetGamesWithDetails();

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

            var addedGame = await _gameRepository.AddAsync(game);

            await AddGenresToGame(gameRequest.GenreIds, addedGame);
            await AddPlatformsToGame(gameRequest.PlatformIds, addedGame);

            await _gameRepository.UpdateAsync(addedGame);

            return _mapper.Map<GameView>(addedGame);
        }

        public async Task DeleteByIdAsync(Guid gameId)
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

            await AddGenresToGame(gameRequest.GenreIds, game);
            await AddPlatformsToGame(gameRequest.PlatformIds, game);

            var isUpdated = await _gameRepository.UpdateAsync(game);

            if (!isUpdated)
            {
                throw new ServerErrorException("Can't update this game.", null);
            }
        }    

        private async Task AddGenresToGame(IEnumerable<Guid> genresIds, Game game)
        {
            foreach (var genreId in genresIds)
            {
                var genre = await _genreRepository.GetByIdAsync(genreId);
                ThrowIfEntityIsNull(genre);

                game.Genres.Add(genre!);
            }
        }

        private async Task AddPlatformsToGame(IEnumerable<Guid> platformsIds, Game game)
        {
            foreach (var platformId in platformsIds)
            {
                var platformType = await _platformRepository.GetByIdAsync(platformId);
                ThrowIfEntityIsNull(platformType);

                game.Platforms.Add(platformType!);
            }
        }

        private static void ThrowIfEntityIsNull<TEntity>(TEntity? entity)
            where TEntity : class, IEntity<Guid>
        {
            if (entity is null)
            {
                throw new NotFoundException($"{typeof(TEntity).Name} with such id doesn't exist.");
            }
        }
    }
}
