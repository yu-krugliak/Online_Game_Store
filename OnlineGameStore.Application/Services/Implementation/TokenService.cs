using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineGameStore.Application.Auth.JwtTokenServices;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OnlineGameStore.Application.Exceptions;

namespace OnlineGameStore.Application.Services.Implementation;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;

    public TokenService(UserManager<User> userManager, IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<TokenView> GetTokenAsync(TokenRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email.Trim().Normalize()) is not { } user ||
            !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedException("Wrong email or password.");
        }

        return GenerateTokenResponse(user);
    }

    private TokenView GenerateTokenResponse(User user)
    {
        var token = GenerateToken(user);

        return new TokenView(
            ExpiryTime: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            Token: token,
            RefreshToken: string.Empty
        );
    }

    private string GenerateToken(User user)
    {
        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName!),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}