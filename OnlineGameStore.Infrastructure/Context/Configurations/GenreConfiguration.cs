using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Context.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(genre => genre.Id);

            builder.Property(genre => genre.Name);
            builder.Property(genre => genre.Description);

            builder.HasOne(genre => genre.ParentGenre)
                .WithMany(parent => parent.NestedGenres)
                .HasForeignKey(genre => genre.ParentGenreId)
                .IsRequired(false);

            builder.HasMany(genre => genre.NestedGenres)
                .WithOne(child => child.ParentGenre)
                .HasForeignKey(child => child.ParentGenreId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.HasMany(genre => genre.Games)
                .WithMany(game => game.Genres);
        }
    }
}
