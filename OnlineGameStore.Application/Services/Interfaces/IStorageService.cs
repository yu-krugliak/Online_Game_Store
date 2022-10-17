using Microsoft.AspNetCore.Http;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadImageAsync(IFormFile image, string folder);
    }
}
