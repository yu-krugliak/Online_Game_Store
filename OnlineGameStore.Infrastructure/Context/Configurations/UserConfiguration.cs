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

        builder.Property(user => user.FirstName);
        builder.Property(user => user.LastName);
        builder.Property(user => user.UserName);
        builder.Property(user => user.PasswordHash);

        builder.Property(user => user.Email);
        builder.Property(user => user.PhoneNumber);
        builder.Property(user => user.RegistrationDate);
        builder.Property(user => user.AvatarUrl);

        builder.HasMany(user => user.Comments)
            .WithOne(comment => comment.User)
            .HasForeignKey(comment => comment.UserIdCreated)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}