using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IPlatformService : IService<PlatformType> 
    {
        Task<IEnumerable<PlatformView>> GetAllAsync();

        Task<PlatformView> AddAsync(PlatformRequest platformRequest);
    }
}