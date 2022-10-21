using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Constants;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Services
{
    public sealed class GameServiceTests : ServiceTestsBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlatformRepository _platformRepository;
        private readonly IGenreRepository _genreRepository;

        private readonly GameService _sut;
        private readonly IStorageService _storageService;

        public GameServiceTests()
        {
            _gameRepository = Substitute.For<IGameRepository>();
            _genreRepository = Substitute.For<IGenreRepository>();
            _platformRepository = Substitute.For<IPlatformRepository>();
            _storageService = Substitute.For<IStorageService>();

            _sut = new(_gameRepository, _genreRepository, _platformRepository, _storageService, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoErrors_ShouldReturnGamesViews()
        {
            // Arrange
            var games = _fixture.CreateMany<Game>(3).ToList();
            var gameViews = _mapper.Map<IEnumerable<GameView>>(games).ToList();
            _gameRepository.GetGamesWithDetails().Returns(games);

            // Act
            var result = (await _sut.GetAllAsync()).ToList();

            // Assert
            await _gameRepository.Received(1).GetGamesWithDetails();
            result.Should().BeEquivalentTo(gameViews);
        }

        [Fact]
        public async Task GetByGenresAndNameAsync_WhenGamesExists_ShouldReturnGameViewList()
        {
            // Arrange
            var ids = _fixture.CreateMany<int>(3).ToList();
            const string name = "name";

            var games = _fixture.CreateMany<Game>(3);
            var gameViews = _mapper.Map<IEnumerable<GameView>>(games);
            _gameRepository.FilterGamesByGenresAndNameAsync(Arg.Any<List<int>>(), Arg.Any<string>())
                .Returns(games);

            // Act
            var result = await _sut.GetByGenresAndNameAsync(ids, name);

            // Assert
            await _gameRepository.Received(1).FilterGamesByGenresAndNameAsync(ids, name);
            result.Should().BeEquivalentTo(gameViews);
        }

        [Fact]
        public async Task GetByIdAsync_WhenGameExists_ShouldReturnGameView()
        {
            // Arrange
            var id = _fixture.Create<int>();

            var game = _fixture.Create<Game>();
            var gameView = _mapper.Map<GameView>(game);
            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>()).Returns(game);

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            await _gameRepository.Received(1).GetGameByIdWithDetails(id);
            result.Should().BeEquivalentTo(gameView);
        }

        [Fact]
        public async Task GetByIdAsync_WhenGameNotExists_ShouldThrowNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>()).Returns((Game?)null);

            // Act
            Func<Task> act = async () => await _sut.GetByIdAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Game with such id doesn't exist.");

            await _gameRepository.Received(1).GetGameByIdWithDetails(id);
        }

        [Fact]
        public async Task AddAsync_WhenNoErrors_ShouldPassToRepository()
        {
            // Arrange
            var genres = _fixture.CreateMany<Genre>(3).ToList();
            var platforms = _fixture.CreateMany<PlatformType>(3).ToList();
            
            var expectedGame = _fixture.Build<Game>()
                .With(g => g.Genres, genres)
                .With(game => game.Platforms, platforms)
                .Without(g => g.ImageUrl)
                .Create();

            var gameRequest = _mapper.Map<GameRequest>(expectedGame);
            gameRequest.GenreIds = expectedGame.Genres.Select(g => g.Id).ToList();
            gameRequest.PlatformIds = expectedGame.Platforms.Select(g => g.Id).ToList();

            var gameToAdd = _mapper.Map<Game>(gameRequest);
            gameToAdd.Id = expectedGame.Id;

            var gameView = _mapper.Map<GameView>(expectedGame);

            _genreRepository.GetByIdAsync(Arg.Any<int>()).ReturnsForAnyArgs(x => genres.Find(genre => genre.Id == x.Arg<int>()));
            _platformRepository.GetByIdAsync(Arg.Any<int>()).ReturnsForAnyArgs(x => platforms.Find(pl => pl.Id == x.Arg<int>()));

            _gameRepository.AddAsync(Arg.Any<Game>()).Returns(gameToAdd);
            _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(true);

            // Act
            var result = await _sut.AddAsync(gameRequest);

            // Assert
            result.Should().BeEquivalentTo(gameView);
            await _gameRepository.Received(1).AddAsync(Arg.Any<Game>());
            await _gameRepository.Received(1).UpdateAsync(Arg.Any<Game>());
        }

        [Fact]
        public async Task DeleteByIdAsync_WhenGameExists_ShouldDeleteInRepository()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _gameRepository.DeleteByIdAsync(Arg.Any<int>()).Returns(true);

            // Act
            await _sut.DeleteByIdAsync(id);

            // Assert
            await _gameRepository.Received(1).DeleteByIdAsync(id);
        }

        [Fact]
        public async Task DeleteByIdAsync_WhenGameNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _gameRepository.DeleteByIdAsync(Arg.Any<int>()).Returns(false);

            // Act
            Func<Task> action = async () => await _sut.DeleteByIdAsync(id);

            // Assert
            await action.Should().ThrowAsync<NotFoundException>()
                .WithMessage("This game doesn't exist.");
            await _gameRepository.Received(1).DeleteByIdAsync(id);
        }

        [Fact]
        public async Task UpdateAsync_WhenNoErrors_ShouldPassToRepository()
        {
            // Arrange
            var genres = _fixture.CreateMany<Genre>(3).ToList();
            var platforms = _fixture.CreateMany<PlatformType>(3).ToList();

            var expectedGame = _fixture.Build<Game>()
                .With(g => g.Genres, genres)
                .With(game => game.Platforms, platforms)
                .Without(g => g.ImageUrl)
                .Create();

            var gameRequest = _mapper.Map<GameRequest>(expectedGame);
            
            gameRequest.GenreIds = expectedGame.Genres.Select(g => g.Id).ToList();
            gameRequest.PlatformIds = expectedGame.Platforms.Select(g => g.Id).ToList();

            _genreRepository.GetByIdAsync(Arg.Any<int>()).ReturnsForAnyArgs(x => genres.Find(genre => genre.Id == x.Arg<int>()));
            _platformRepository.GetByIdAsync(Arg.Any<int>()).ReturnsForAnyArgs(x => platforms.Find(pl => pl.Id == x.Arg<int>()));
            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>()).Returns(expectedGame);
            _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(true);

            // Act
            await _sut.UpdateAsync(expectedGame.Id, gameRequest);

            // Assert
            await _gameRepository.Received(1).UpdateAsync(Arg.Any<Game>());
        }

        [Fact]
        public async Task UpdateAsync_WhenGameNotExists_ShouldThrowNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var gameRequest = _fixture.Create<GameRequest>();
            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>()).Returns((Game?)null);

            // Act
            Func<Task> act = async () => await _sut.UpdateAsync(id, gameRequest);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Game with such id doesn't exist.");

            await _gameRepository.Received(1).GetGameByIdWithDetails(id);
        }

        [Fact]
        public async Task UpdateAsync_WhenUpdateError_ShouldThrowServerError()
        {
            // Arrange
            var gameRequest = _fixture.Build<GameRequest>()
                .Without(game => game.PlatformIds)
                .Without(game => game.GenreIds)
                .Create();

            var game = _mapper.Map<Game>(gameRequest);
            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>()).Returns(game);
            _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(false);

            // Act
            Func<Task> act = async () => await _sut.UpdateAsync(game.Id, gameRequest);

            // Assert
            await act.Should()
                .ThrowAsync<ServerErrorException>()
                .WithMessage("Can't update this game.");

            await _gameRepository.Received(1).GetGameByIdWithDetails(Arg.Any<int>());
            await _gameRepository.Received(1).UpdateAsync(Arg.Any<Game>());
        }

        [Fact]
        public async Task UpdateImageAsync_WhenGameNotExists_ShouldThrowNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>()).Returns((Game?)null);

            // Act
            Func<Task> act = async () => await _sut.UpdateImageAsync(id, default!);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Game with such id doesn't exist.");

            await _gameRepository.Received(1).GetGameByIdWithDetails(id);
        }

        [Fact]
        public async Task UpdateImageAsync_WhenGameExistsAndFailedToUpdate_ShouldThrowServerErrorException()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var game = _fixture.Create<Game>();

            const string url = "https://youtu.be/dQw4w9WgXcQ";

            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>())
                .Returns(game);

            _storageService.UploadImageAsync(Arg.Any<IFormFile>(), Arg.Any<string>())
                .Returns(url);

            _gameRepository.UpdateAsync(Arg.Any<Game>())
                .Returns(false);

            // Act
            Func<Task> act = async () => await _sut.UpdateImageAsync(id, default!);

            // Assert
            await act.Should()
                .ThrowAsync<ServerErrorException>()
                .WithMessage("Can't add image to this game.");

            await _gameRepository.Received(1).GetGameByIdWithDetails(id);
            await _storageService.Received(1).UploadImageAsync(default!, FolderNamesConstants.GamesPictures);
            await _gameRepository.Received(1).UpdateAsync(game);
        }

        [Fact]
        public async Task UpdateImageAsync_WhenGameExists_ShouldSetGameImage()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var game = _fixture.Create<Game>();

            const string url = "https://youtu.be/dQw4w9WgXcQ";

            _gameRepository.GetGameByIdWithDetails(Arg.Any<int>())
                .Returns(game);

            _storageService.UploadImageAsync(Arg.Any<IFormFile>(), Arg.Any<string>())
                .Returns(url);

            _gameRepository.UpdateAsync(Arg.Any<Game>())
                .Returns(true);

            // Act
            await _sut.UpdateImageAsync(id, default!);

            // Assert
            game.ImageUrl.Should().Be(url);

            await _gameRepository.Received(1).GetGameByIdWithDetails(id);
            await _storageService.Received(1).UploadImageAsync(default!, FolderNamesConstants.GamesPictures);
            await _gameRepository.Received(1).UpdateAsync(game);
        }
    }
}
