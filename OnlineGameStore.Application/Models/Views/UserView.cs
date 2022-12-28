namespace OnlineGameStore.Application.Models.Views
{
    public record UserView(Guid Id, string NormalizedEmail, string FirstName, string LastName, 
        string NormalizedUserName, string? AvatarUrl);
}