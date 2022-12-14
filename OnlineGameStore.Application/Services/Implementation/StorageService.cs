using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using OnlineGameStore.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using OnlineGameStore.Application.Configurations;
using OnlineGameStore.Application.Services.Constants;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class StorageService : IStorageService
    {
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
                Folder = Path.Combine(FolderNamesConstants.Root, folder).Replace("\\", "/")
            }).ConfigureAwait(false);

            return result.Url.OriginalString;
        }
    }
}
