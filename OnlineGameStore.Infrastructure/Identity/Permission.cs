using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Identity;

public class Permission : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }


    public virtual ICollection<RolePermission>? RolePermissions { get; set; }
}