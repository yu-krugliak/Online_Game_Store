using Mapster;
using Microsoft.Extensions.DependencyInjection;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Application.Mapster
{
    public static class MapsterConfiguration
    {
        public static TypeAdapterConfig GetConfiguration()
        {
            var config = new TypeAdapterConfig();

            config.ForType<Game, GameView>()
                .IgnoreNullValues(false);

            config.ForType<GameRequest, Game>()
                .IgnoreNullValues(true);

            config.ForType<Comment, CommentView>()
                .IgnoreNullValues(false);

            config.ForType<CommentRequest, Comment>()
                .IgnoreNullValues(true);

            config.ForType<Genre, GenreView>()
                .IgnoreNullValues(false);

            config.ForType<GenreRequest, Genre>()
                .IgnoreNullValues(true);

            config.ForType<PlatformType, PlatformView>()
                .IgnoreNullValues(false);

            config.ForType<PlatformRequest, PlatformType>()
                .IgnoreNullValues(true);

            return config;
        }

        public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
        {
            var config = GetConfiguration();
            return services.AddSingleton(config);
        }
    }
}