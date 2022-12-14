using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using OnlineGameStore.Application.Configurations;
using OnlineGameStore.Application.Mapster;
using OnlineGameStore.Application.Models.Validators;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Identity;

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
                    .AddMapster()
                    .AddIdentity()
                    .AddJwtAuth()
                    .AddCurrentUser()
                    .AddValidation();

            return builder;
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder builder)
        {
            return builder
                .UseAuthentication()
                .UseAuthorization()
                .UseCurrentUser();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IGameService, GameService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IGenreService, GenreService>()
                .AddTransient<IPlatformService, PlatformService>()
                .AddTransient<ITokenService, TokenService>()
                .AddTransient<IUserService, UserService>();
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<GamesContext>()
                .AddDefaultTokenProviders()
                .Services;
        }

        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            return services
                .AddMapsterConfiguration()
                .AddTransient<IMapper, Mapper>();
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            return services
                .AddValidatorsFromAssemblyContaining<CommentRequestValidator>()
                .AddValidatorsFromAssemblyContaining<GameRequestValidator>()
                .AddValidatorsFromAssemblyContaining<GenreRequestValidator>()
                .AddValidatorsFromAssemblyContaining<PlatformRequestValidator>()
                .AddValidatorsFromAssemblyContaining<RefreshTokenRequestValidator>()
                .AddValidatorsFromAssemblyContaining<RegisterRequestValidator>()
                .AddValidatorsFromAssemblyContaining<TokenRequestValidator>();
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