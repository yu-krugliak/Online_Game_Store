using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using OnlineGameStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Repositories.Implementations;
using OnlineGameStore.Api.Configurations;

namespace OnlineGameStore.Infrastructure
{
    public static class StartupInfrastructure
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<GamesContext>((provider, options) =>
            {
                var connectionConfig = builder.Configuration
                    .GetSection(DbConnectionConfiguration.SectionName)
                    .Get<DbConnectionConfiguration>();

                options.UseSqlServer(connectionConfig.MsSqlConnectionString);
            })
            .AddRepositories();

            return builder;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IGameRepository, GameRepository>()
                .AddTransient<ICommentRepository, CommentRepository>()
                .AddTransient<IGenreRepository, GenreRepository>()
                .AddTransient<IPlatformRepository, PlatformRepository>();
        }
    }
}
