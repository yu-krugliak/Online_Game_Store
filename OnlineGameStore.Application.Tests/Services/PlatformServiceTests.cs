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
    public sealed class PlatformServiceTests : ServiceTestsBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly PlatformService _sut;

        public PlatformServiceTests()
        {
            _platformRepository = Substitute.For<IPlatformRepository>();
            _sut = new(_platformRepository, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoErrors_ShouldReturnPlatformsViews()
        {
            // Arrange
            var platforms = _fixture.CreateMany<PlatformType>(3).ToList();
            var platformsViews = _mapper.Map<IEnumerable<PlatformView>>(platforms).ToList();
            _platformRepository.GetAllAsync().Returns(platforms);

            // Act
            var result = (await _sut.GetAllAsync()).ToList();

            // Assert
            await _platformRepository.Received(1).GetAllAsync();
            result.Should().BeEquivalentTo(platformsViews);
        }

        [Fact]
        public async Task GetByIdAsync_WhenPlatformExists_ShouldReturnPlatformView()
        {
            // Arrange
            var id = _fixture.Create<int>();

            var platform = _fixture.Create<PlatformType>();
            var platformView = _mapper.Map<PlatformView>(platform);
            _platformRepository.GetByIdAsync(Arg.Any<int>()).Returns(platform);

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            await _platformRepository.Received(1).GetByIdAsync(id);
            result.Should().BeEquivalentTo(platformView);
        }

        [Fact]
        public async Task GetByIdAsync_WhenPlatformNotExists_ShouldThrowNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _platformRepository.GetByIdAsync(Arg.Any<int>()).Returns((PlatformType?)null);

            // Act
            Func<Task> act = async () => await _sut.GetByIdAsync(id);

            // Assert
            await act.Should()
            .ThrowAsync<NotFoundException>()
                .WithMessage("PlatformType with such id doesn't exist.");

            await _platformRepository.Received(1).GetByIdAsync(id);
        }

        [Fact]
        public async Task AddAsync_WhenNoErrors_ShouldPassToRepository()
        {
            // Arrange
            var platformRequest = _fixture.Create<PlatformRequest>();
            var platform = _mapper.Map<PlatformType>(platformRequest);
            var platformView = _mapper.Map<PlatformView>(platform);

            _platformRepository.AddAsync(Arg.Any<PlatformType>()).Returns(platform);

            // Act
            var result = await _sut.AddAsync(platformRequest);

            // Assert
            result.Should().BeEquivalentTo(platformView);
            await _platformRepository.Received(1).AddAsync(Arg.Any<PlatformType>());
        }
    }
}
