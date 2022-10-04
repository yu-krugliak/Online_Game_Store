using FluentAssertions;
using MapsterMapper;
using Moq;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Service
{
    public class GenreServiceTests
    {
        private readonly Mock<IGenreRepository> _genreRepository;
        private readonly IGenreService _sut;
        private readonly Mock<IMapper> _mapperMock;

        public GenreServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _genreRepository = new Mock<IGenreRepository>();
            _sut = new GenreService(_genreRepository.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetGenres_WhenListIsEmpty_ThenEmptyListReturned()
        {
            var result = await _sut.GetAllAsync();
            result.Should().BeNullOrEmpty();

            _genreRepository.Verify(x => x.GetAllAsync(), Times.Once);
            _genreRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetGenres_WhenListIsNotEmpty_ThenListReturned()
        {
            IEnumerable<Genre> listGenres = Enumerable.Empty<Genre>();
            listGenres.Append(
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "Strategy",
                    Description = "Exciting strategy",
                });
            listGenres.Append(
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "RPG",
                    Description = "Exciting RPG",
                }
            );

            IEnumerable<GenreView> listGenresViews = Enumerable.Empty<GenreView>();
            listGenresViews.Append(
                new GenreView()
                {
                    Id = Guid.NewGuid(),
                    Name = "Strategy",
                    Description = "Exciting strategy",
                });
            listGenresViews.Append(
                new GenreView()
                {
                    Id = Guid.NewGuid(),
                    Name = "RPG",
                    Description = "Exciting RPG",
                }
            );

            _genreRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(listGenres));
            var result = await _sut.GetAllAsync();

            _mapperMock.Setup(maper => maper.Map<IEnumerable<GenreView>>(It.IsAny<IEnumerable<Genre>>()))
               .Returns(listGenresViews);
            result.Should().Equal(listGenresViews);

            _genreRepository.Verify(x => x.GetAllAsync(), Times.Once);
            _genreRepository.VerifyNoOtherCalls();
        }
    }
}
