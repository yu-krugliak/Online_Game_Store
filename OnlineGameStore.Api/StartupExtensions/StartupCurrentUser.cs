using OnlineGameStore.Application.Auth;
using OnlineGameStore.Application.Middleware;

namespace OnlineGameStore.Api.StartupExtensions
{
    public static class StartupCurrentUser
    {
        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            return services
                .AddScoped<CurrentUserMiddleware>()
                .AddScoped<ICurrentUser, CurrentUser>();
        }

        public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<CurrentUserMiddleware>();
        }
    }
}
