using Microsoft.AspNetCore.Identity;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Identity;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? Avatar { get; set; }


    public virtual ICollection<Comment>? Comments { get; set; }
}