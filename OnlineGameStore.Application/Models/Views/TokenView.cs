namespace OnlineGameStore.Application.Models.Views;

public record TokenView(string AccessToken, DateTime AccessTokenExpiryTime, string RefreshToken, DateTime RefreshTokenExpiryTime);