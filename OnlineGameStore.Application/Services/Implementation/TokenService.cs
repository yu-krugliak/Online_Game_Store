using Microsoft.AspNetCore.Identity;
using OnlineGameStore.Application.Auth.JwtTokenServices;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Application.Services.Implementation;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    public Task<TokenView> GetTokenAsync(TokenRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}