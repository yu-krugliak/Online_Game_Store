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
    public class PlatformServiceTests
    {
        private readonly Mock<IPlatformRepository> _platformRepository;
        private readonly IPlatformService _sut;
        private readonly Mock<IMapper> _mapperMock;

        public PlatformServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _platformRepository = new Mock<IPlatformRepository>();
            _sut = new PlatformService(_platformRepository.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetPlatforms_WhenListIsEmpty_ThenEmptyListReturned()
        {
            var result = await _sut.GetAllAsync();
            result.Should().BeNullOrEmpty();

            _platformRepository.Verify(x => x.GetAllAsync(), Times.Once);
            _platformRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetPlatforms_WhenListIsNotEmpty_ThenListReturned()
        {
            IEnumerable<PlatformType> listPlatforms = Enumerable.Empty<PlatformType>();
            listPlatforms.Append(
                new PlatformType()
                {
                    Id = Guid.NewGuid(),
                    Type = "Mobile",
                });
            listPlatforms.Append(
                new PlatformType()
                {
                    Id = Guid.NewGuid(),
                    Type = "Desktop",
                }
            );

            IEnumerable<PlatformView> listPlatformsViews = Enumerable.Empty<PlatformView>();
            listPlatformsViews.Append(
                new PlatformView()
                {
                    Id = Guid.NewGuid(),
                    Type = "Mobile",
                });
            listPlatformsViews.Append(
                new PlatformView()
                {
                    Id = Guid.NewGuid(),
                    Type = "Desktop",
                }
            );

            _platformRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(listPlatforms));
            var result = await _sut.GetAllAsync();

            _mapperMock.Setup(maper => maper.Map<IEnumerable<PlatformView>>(It.IsAny<IEnumerable<PlatformType>>()))
               .Returns(listPlatformsViews);
            result.Should().Equal(listPlatformsViews);

            _platformRepository.Verify(x => x.GetAllAsync(), Times.Once);
            _platformRepository.VerifyNoOtherCalls();
        }
    }
}
