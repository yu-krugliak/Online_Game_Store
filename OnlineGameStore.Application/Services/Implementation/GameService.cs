using MapsterMapper;
using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Application.Services.UnitOfWorkImplementation;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GameView> AddAsync(GameRequest gameRequest)
        {
            var game = _mapper.Map<Game>(gameRequest);

            var addedGame = await _unitOfWork.GameRepository.AddAsync(game);

            return _mapper.Map<GameView>(addedGame);
        }

        public async Task DeleteByKeyAsync(Guid gameKey)
        {
            var result = await _unitOfWork.GameRepository.DeleteByIdAsync(gameKey);

            if (!result)
            {
                throw new NotFoundException("This game doesn't exist.");
            }
        }

        public async Task<IEnumerable<GameView>> GetAllAsync()
        {
            var games = await _unitOfWork.GameRepository.GetAllAsync();

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task<IEnumerable<GameView>> GetByGenre(Guid genreId)
        {
            var genre = await _unitOfWork.GenreRepository.GetGenreByIdWithDetails(genreId);
            var games = genre.Games;

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task<GameView> GetByIdAsync(Guid gameKey)
        {
            var game = await GetExistingGameById(gameKey);

            var gameView = _mapper.Map<GameView>(game);
            return gameView;
        }

        public async Task<IEnumerable<GameView>> GetByPlatform(Guid platformId)
        {
            var platformType = await _unitOfWork.PlatformRepository.GetPlatformByIdWithDetails(platformId);
            var games = platformType.Games;

            var gamesViews = _mapper.Map<IEnumerable<GameView>>(games);
            return gamesViews;
        }

        public async Task UpdateAsync(Guid gameKey, GameRequest gameRequest)
        {
            var game = await GetExistingGameById(gameKey);
            _mapper.Map(gameRequest, game);

            var result = await _unitOfWork.GameRepository.UpdateAsync(game);

            if (!result)
            {
                throw new ServerErrorException("Can't update this game.", null);
            }
        }

        private async Task<Game> GetExistingGameById(Guid id)
        {
            var entity = await _unitOfWork.GameRepository.GetByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"Game with such id doesn't exist.");
            }

            return entity;
        }
    }
}
