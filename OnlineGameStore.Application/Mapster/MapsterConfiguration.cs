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
                .Map(gv => gv.GenreIds, g => SelectIdsFromCollection(g.Genres))
                .Map(gv => gv.PlatformIds, g => SelectIdsFromCollection(g.Platforms))
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

        private static IEnumerable<Guid>? SelectIdsFromCollection<TEntity>(IEnumerable<TEntity>? collection)
            where TEntity : class, IEntity<Guid>
        {
            return collection?.Select(entity => entity.Id);
        }
    }
}