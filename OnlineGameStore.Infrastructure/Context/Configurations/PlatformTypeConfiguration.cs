using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Context.Configurations
{
    public class PlatformTypeConfiguration : IEntityTypeConfiguration<PlatformType>
    {
        public void Configure(EntityTypeBuilder<PlatformType> builder)
        {
            builder.HasKey(platform => platform.Id);

            builder.Property(platform => platform.Type);

            builder.HasMany(platform => platform.Games)
                .WithMany(game => game.Platforms);
        }
    }
}
