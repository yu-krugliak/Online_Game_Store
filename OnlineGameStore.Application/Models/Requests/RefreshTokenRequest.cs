namespace OnlineGameStore.Application.Models.Requests;

public record RefreshTokenRequest(string? AccessToken, string? RefreshToken);