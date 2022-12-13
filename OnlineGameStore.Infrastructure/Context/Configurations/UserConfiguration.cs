using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Infrastructure.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.FirstName)
            .IsRequired(true).HasMaxLength(100);

        builder.Property(user => user.LastName)
            .IsRequired(true).HasMaxLength(100);

        builder.Property(user => user.RegistrationDate);

        builder.Property(user => user.AvatarUrl)
            .HasMaxLength(2083);
        
        builder.Property(user => user.RefreshToken)
            .IsRequired(false);

        builder.Property(user => user.RefreshTokenExpiryTime);

        builder.HasMany(user => user.Comments)
            .WithOne(comment => comment.User)
            .HasForeignKey(comment => comment.UserIdCreated)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}