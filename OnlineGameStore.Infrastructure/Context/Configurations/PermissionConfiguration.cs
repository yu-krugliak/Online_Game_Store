using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Infrastructure.Context.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(permission => permission.Id);

        builder.Property(permission => permission.Name);
        builder.Property(permission => permission.Description);
    }
}