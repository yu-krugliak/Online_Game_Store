using Microsoft.AspNetCore.Http;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserView> GetUserByEmail(string email);

        Task RegisterAsync(RegisterRequest request);

        Task UpdateAvatarAsync(IFormFile image);
    }
}
