using Microsoft.AspNetCore.Http;

namespace OnlineGameStore.Application.Helpers
{
    public interface IImageControl
    {
        Task<string> UploadImageAsync(IFormFile image, string folder);
    }
}