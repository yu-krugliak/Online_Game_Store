using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Api.Configurations;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Repositories.Implementations;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Api.StartupExtensions
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
