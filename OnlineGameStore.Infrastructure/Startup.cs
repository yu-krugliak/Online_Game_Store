using Microsoft.Extensions.DependencyInjection;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGameStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineGameStore.Infrastructure.Repositories.Implementations;

namespace OnlineGameStore.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<GamesContext>((provider, options) =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection");
                options.UseSqlServer(connectionString);
            })
            .AddRepositories();
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
