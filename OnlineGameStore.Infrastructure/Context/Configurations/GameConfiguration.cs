using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Context.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(game => game.Id);

            builder.Property(game => game.Key)
                .IsRequired(true).HasMaxLength(100);

            builder.Property(game => game.Name)
                .IsRequired(true).HasMaxLength(100);

            builder.Property(game => game.Description)
                .IsRequired(true).HasMaxLength(500);

            builder.Property(game => game.Price)
                .HasPrecision(7, 2);

            builder.Property(game => game.ImageUrl)
                .HasMaxLength(2083);

        builder.HasMany(game => game.Comments)
                .WithOne(comment => comment.Game)
                .HasForeignKey(comment => comment.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasMany(game => game.Genres)
                .WithMany(genre => genre.Games);

            builder.HasMany(game => game.Platforms)
                .WithMany(type => type.Games);
        }
    }
}
