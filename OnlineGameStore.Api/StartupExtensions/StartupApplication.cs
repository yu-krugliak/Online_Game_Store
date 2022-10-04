using MapsterMapper;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;

namespace OnlineGameStore.Api.StartupExtensions
{
    public static class StartupApplication
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
