using CloudinaryDotNet;
using MapsterMapper;
using OnlineGameStore.Api.Configurations;
using OnlineGameStore.Application.Exeptions;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;

namespace OnlineGameStore.Api.StartupExtensions
{
    public static class StartupApplication
    {
        public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
        {
            builder
                .AddCloudinary()
                .Services
                    .AddServices()
                    .AddMapster();

            return builder;
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

        public static WebApplicationBuilder AddCloudinary(this WebApplicationBuilder builder)
        {
            builder.Services
                .Configure<CloudinaryAccountOptions>(
                    builder.Configuration.GetSection(CloudinaryAccountOptions.SectionName)
                );

            builder.Services.AddTransient<IStorageService, StorageService>();
            return builder;
        }
    }
}
