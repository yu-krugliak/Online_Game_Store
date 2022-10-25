using AutoFixture;
using FluentAssertions;
using NSubstitute;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Services
{
    public sealed class GenreServiceTests : ServiceTestsBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly GenreService _sut;

        public GenreServiceTests()
        {
            _genreRepository = Substitute.For<IGenreRepository>();
            _sut = new(_genreRepository, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoErrors_ShouldReturnGenresViews()
        {
            // Arrange
            var genres = _fixture.CreateMany<Genre>(3).ToList();
            var genresViews = _mapper.Map<IEnumerable<GenreView>>(genres).ToList();
            _genreRepository.GetAllAsync().Returns(genres);

            // Act
            var result = (await _sut.GetAllAsync()).ToList();

            // Assert
            await _genreRepository.Received(1).GetAllAsync();
            result.Should().BeEquivalentTo(genresViews);
        }

        [Fact]
        public async Task GetByIdAsync_WhenGenreExists_ShouldReturnGenreView()
        {
            // Arrange
            var id = _fixture.Create<int>();

            var genre = _fixture.Create<Genre>();
            var genreView = _mapper.Map<GenreView>(genre);
            _genreRepository.GetByIdAsync(Arg.Any<int>()).Returns(genre);

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            await _genreRepository.Received(1).GetByIdAsync(id);
            result.Should().BeEquivalentTo(genreView);
        }

        [Fact]
        public async Task GetByIdAsync_WhenGenreNotExists_ShouldThrowNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _genreRepository.GetByIdAsync(Arg.Any<int>()).Returns((Genre?)null);

            // Act
            Func<Task> act = async () => await _sut.GetByIdAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Genre with such id doesn't exist.");

            await _genreRepository.Received(1).GetByIdAsync(id);
        }

        [Fact]
        public async Task AddAsync_WhenNoErrors_ShouldPassToRepository()
        {
            // Arrange
            var genreRequest = _fixture.Create<GenreRequest>();
            var genre = _mapper.Map<Genre>(genreRequest);
            var genreView = _mapper.Map<GenreView>(genre);

            _genreRepository.AddAsync(Arg.Any<Genre>()).Returns(genre);

            // Act
            var result = await _sut.AddAsync(genreRequest);

            // Assert
            result.Should().BeEquivalentTo(genreView);
            await _genreRepository.Received(1).AddAsync(Arg.Any<Genre>());
        }
    }
}
