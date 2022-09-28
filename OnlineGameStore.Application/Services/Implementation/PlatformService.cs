using MapsterMapper;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Repositories.Implementations;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class PlatformService : ServiceBase<PlatformType>, IPlatformService 
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformService(IPlatformRepository platformRepository, IMapper mapper) : base(platformRepository)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlatformView>> GetAllAsync()
        {
            var platforms = await _platformRepository.GetAllAsync();

            var platformViews = _mapper.Map<IEnumerable<PlatformView>>(platforms);
            return platformViews;
        }

        public async Task<PlatformView> AddAsync(PlatformRequest platformRequest)
        {
            var platform = _mapper.Map<PlatformType>(platformRequest);

            var addedPlatform = await _platformRepository.AddAsync(platform);

            return _mapper.Map<PlatformView>(addedPlatform);
        }
    }
}
