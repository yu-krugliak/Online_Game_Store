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
using System.Security.Cryptography;
using OnlineGameStore.Application.Auth;

namespace OnlineGameStore.Application.Services.Implementation;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly ICurrentUser _currentUser;

    public TokenService(UserManager<User> userManager, IOptions<JwtSettings> jwtOptions, ICurrentUser currentUser)
    {
        _userManager = userManager;
        _jwtSettings = jwtOptions.Value;
        _currentUser = currentUser;
    }

    public async Task<TokenView> GetTokenAsync(TokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email.Trim());
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedException("Wrong email or password.");
        }

        return await GenerateTokenResponse(user);
    }

    public async Task<TokenView> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken!);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new NotFoundException("User with such token not found.");

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new NotFoundException("User with such token not found.");
        }

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new InvalidRequestException("Invalid client request. Unable to refresh token.");
        }

        return await GenerateTokenResponse(user);
    }

    public async Task<bool> RevokeTokenAsync()
    {
        var userId = _currentUser.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user is null)
        {
            throw new NotFoundException("This user doesn't exist.");
        }

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
        return true;
    }

    private async Task<TokenView> GenerateTokenResponse(User user)
    {
        var accessToken = GenerateToken(user);
        var refreshToken = GenerateRefreshToken();
        var accessExpiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);
        var refreshExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        await SaveUserToken(user, refreshToken, refreshExpiryTime);

        return new TokenView(
            AccessToken: accessToken,
            AccessTokenExpiryTime: accessExpiryTime, 
            RefreshToken: refreshToken,
            RefreshTokenExpiryTime: refreshExpiryTime
        );
    }   
    
    private async Task SaveUserToken(User user, string refreshToken, DateTime expiryTime)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = expiryTime;

        await _userManager.UpdateAsync(user);
    }

    private string GenerateToken(User user)
    {
        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
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

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rnGenerator = RandomNumberGenerator.Create();
        rnGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            RoleClaimType = ClaimTypes.Role,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.
            Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}