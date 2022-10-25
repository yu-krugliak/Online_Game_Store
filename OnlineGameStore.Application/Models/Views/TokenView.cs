namespace OnlineGameStore.Application.Models.Views;

public record TokenView(string Token, string RefreshToken, DateTime ExpiryTime);