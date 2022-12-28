using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Entities;
using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Infrastructure.Context
{
    public class GamesContext : IdentityDbContext<User, Role, Guid>
    {
        public virtual DbSet<Game> Games { get; set; } = default!;

        public virtual DbSet<Comment> Comments { get; set; } = default!;

        public virtual DbSet<Genre> Genres { get; set; } = default!;

        public virtual DbSet<PlatformType> PlatformTypes { get; set; } = default!;


        public GamesContext(DbContextOptions<GamesContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamesContext).Assembly);
        }
    }
}
