using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Context
{
    public class GamesContext : DbContext
    {
        public virtual DbSet<Game> Games { get; set; } = default!;

        public virtual DbSet<Comment> Comments { get; set; } = default!;

        public virtual DbSet<Genre> Genres { get; set; } = default!;

        public virtual DbSet<PlatformType> PlatformTypes { get; set; } = default!;


        public GamesContext(DbContextOptions<GamesContext> options) : base(options)
        {
            Database?.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamesContext).Assembly);
        }
    }
}
