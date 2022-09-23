using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Context.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(game => game.Key);

            builder.Property(game => game.Name);
            builder.Property(game => game.Description);

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
