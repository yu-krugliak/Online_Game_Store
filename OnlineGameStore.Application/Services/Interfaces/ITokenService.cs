using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;

namespace OnlineGameStore.Application.Services.Interfaces;

public interface ITokenService
{
    Task<TokenView> GetTokenAsync(TokenRequest request, CancellationToken cancellationToken);
}