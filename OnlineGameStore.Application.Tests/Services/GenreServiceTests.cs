using AutoFixture;
using FluentAssertions;
using MapsterMapper;
using NSubstitute;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Services
{
    public class GenreServiceTests
    {
        private readonly IGenreRepository _genreRepository;
        private readonly GenreService _sut;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture = new();

        public GenreServiceTests()
        {
            _genreRepository = Substitute.For<IGenreRepository>();
            _mapper = Substitute.For<IMapper>();
            _mapper = new Mapper(MapsterConfiguration.GetConfiguration());
            _sut = new(_genreRepository, _mapper);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
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
            result.Should().Equal(genresViews, (resView, expView) => resView.Id == expView.Id);
            result.Should().Equal(genresViews, (resView, expView) => resView.Name == expView.Name);
            result.Should().Equal(genresViews, (resView, expView) => resView.Description == expView.Description);
            result.Should().Equal(genresViews, (resView, expView) => resView.ParentGenreId == expView.ParentGenreId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenExists_ShouldReturnGenreView()
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

            result.Id.Should().Be(genreView.Id);
            result.Name.Should().Be(genreView.Name);
            result.Description.Should().Be(genreView.Description);
            result.ParentGenreId.Should().Be(genreView.ParentGenreId);
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
