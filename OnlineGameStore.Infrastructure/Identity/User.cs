using Microsoft.AspNetCore.Identity;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Identity;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? AvatarUrl { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }


    public ICollection<Comment>? Comments { get; set; }
}