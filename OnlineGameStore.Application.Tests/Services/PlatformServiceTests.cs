using AutoFixture;
using FluentAssertions;
using MapsterMapper;
using NSubstitute;
using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Implementations;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace OnlineGameStore.Application.Tests.Services
{
    public class PlatformServiceTests
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly PlatformService _sut;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture = new();

        public PlatformServiceTests()
        {
            _platformRepository = Substitute.For<IPlatformRepository>();
            _mapper = Substitute.For<IMapper>();
            _mapper = new Mapper(MapsterConfiguration.GetConfiguration());
            _sut = new(_platformRepository, _mapper);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
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

            result.Should().Equal(platformsViews, (resView, expView) => resView.Id == expView.Id);
            result.Should().Equal(platformsViews, (resView, expView) => resView.Type == expView.Type);
        }

        [Fact]
        public async Task GetByIdAsync_WhenExists_ShouldReturnPlatformView()
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
            result.Id.Should().Be(platformView.Id);
            result.Type.Should().Be(platformView.Type);
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotExists_ShouldThrowNotFound()
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
