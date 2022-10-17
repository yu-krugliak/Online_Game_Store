using MapsterMapper;
using Microsoft.AspNetCore.Http;
using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Constants;
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
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IGenreRepository genreRepository, 
            IPlatformRepository platformRepository, IStorageService storageService, 
            IMapper mapper) : base(gameRepository)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<IEnumerable<GameView>> GetAllAsync()
        {
            var games = await _gameRepository.GetGamesWithDetails();

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task<IEnumerable<GameView>> GetByGenresAndNameAsync(List<int> genresIds, string? name)
        {
            var games = await _gameRepository.FilterGamesByGenresAndNameAsync(genresIds, name);

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task<GameView> GetByIdAsync(int gameId)
        {
            var game = await _gameRepository.GetGameByIdWithDetails(gameId);
            ThrowIfEntityIsNull(game);

            var gameView = _mapper.Map<GameView>(game!);
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

        public async Task DeleteByIdAsync(int gameId)
        {
            var isDeleted = await _gameRepository.DeleteByIdAsync(gameId);

            if (!isDeleted)
            {
                throw new NotFoundException("This game doesn't exist.");
            }
        }

        public async Task UpdateAsync(int gameId, GameRequest gameRequest)
        {
            var game = await _gameRepository.GetGameByIdWithDetails(gameId);
            ThrowIfEntityIsNull(game);
            _mapper.Map(gameRequest, game);

            await _gameRepository.RemoveGenresFromGame(game!);
            await _gameRepository.RemovePlatformsFromGame(game!);

            await AddGenresToGame(gameRequest.GenreIds, game!);
            await AddPlatformsToGame(gameRequest.PlatformIds, game!);

            var isUpdated = await _gameRepository.UpdateAsync(game!);

            if (!isUpdated)
            {
                throw new ServerErrorException("Can't update this game.", null);
            }
        }

        public async Task UpdateImageAsync(int gameId, IFormFile image)
        {
            var game = await _gameRepository.GetGameByIdWithDetails(gameId);
            ThrowIfEntityIsNull(game);

            var imageUrl = await _storageService.UploadImageAsync(image, FolderNamesConstants.GamesPictures);
            game!.ImageUrl = imageUrl;

            var isUpdated = await _gameRepository.UpdateAsync(game!);

            if (!isUpdated)
            {
                throw new ServerErrorException("Can't add image to this game.", null);
            }
        }

        private async Task AddGenresToGame(IEnumerable<int> genresIds, Game game)
        {
            foreach (var genreId in genresIds.Distinct())
            {
                var genre = await _genreRepository.GetByIdAsync(genreId);
                ThrowIfEntityIsNull(genre);

                game.Genres.Add(genre!);
            }
        }

        private async Task AddPlatformsToGame(IEnumerable<int> platformsIds, Game game)
        {
            foreach (var platformId in platformsIds.Distinct())
            {
                var platformType = await _platformRepository.GetByIdAsync(platformId);
                ThrowIfEntityIsNull(platformType);

                game.Platforms.Add(platformType!);
            }
        }

        private static void ThrowIfEntityIsNull<TEntity>(TEntity? entity)
            where TEntity : class, IEntity<int>
        {
            if (entity is null)
            {
                throw new NotFoundException($"{typeof(TEntity).Name} with such id doesn't exist.");
            }
        }
    }
}
