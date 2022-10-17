using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using OnlineGameStore.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using OnlineGameStore.Api.Configurations;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class StorageService : IStorageService
    {
        private const string Tags = "Game_PhotoAlbum";
        private readonly Cloudinary _cloudinary;

        public StorageService(IOptions<CloudinaryAccountOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options.Value, nameof(options));

            var account = options.Value;
            _cloudinary = new(account);
        }

        public async Task<string> UploadImageAsync(IFormFile image, string folder)
        {
            var result = await _cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                Tags = Tags,
                Folder = $"OnlineGameStoreMedia/{folder}"
            }).ConfigureAwait(false);

            return result.Url.OriginalString;
        }
    }
}
