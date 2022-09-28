using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddServices()
                .AddMapster();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IGameService, GameService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IGenreService, GenreService>()
                .AddTransient<IPlatformService, PlatformService>();
        }

        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            return services
                .AddMapsterConfiguration()
                .AddTransient<IMapper, Mapper>();
        }
    }
}
