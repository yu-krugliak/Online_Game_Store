/*using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineGameStore.Application.Helpers
{
    public class ImageControl : IImageControl
    {
        private const string Tags = "Game_PhotoAlbum";
        private static readonly Cloudinary _cloudinary;//= new("cloudinary://621939247753677:B8fbdvoXtx4EqnIlEbeVL6UKEAI@dxo1brapu");

        //public ImageControl(IOptions<CloudinaryAccountOptions>

        public async Task<string> UploadImageAsync(IFormFile image, string folder)
        {
            var myAccount = new Account { Cloud = "dxo1brapu", ApiKey = "621939247753677", ApiSecret = "B8fbdvoXtx4EqnIlEbeVL6UKEAI" };
            Cloudinary cloudinary = new(myAccount);

            var result = await cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                Tags = Tags,
                Folder = $"OnlineGameStoreMedia/{folder}"
            }).ConfigureAwait(false);

            return result.Url.OriginalString;
        }
    }
}
*/