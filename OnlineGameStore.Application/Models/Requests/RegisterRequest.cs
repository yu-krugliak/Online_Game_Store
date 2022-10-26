namespace OnlineGameStore.Application.Models.Requests;

public record RegisterRequest(string Email, string UserName, string FirstName, string LastName, 
    string Password, string ConfirmPassword);