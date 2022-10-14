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
    }
}
