using Microsoft.AspNetCore.Identity;

namespace OnlineGameStore.Infrastructure.Identity;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }
}