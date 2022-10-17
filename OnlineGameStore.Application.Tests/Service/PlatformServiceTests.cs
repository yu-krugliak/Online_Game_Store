using MapsterMapper;
using Moq;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

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
    }
}
