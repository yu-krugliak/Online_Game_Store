using Microsoft.AspNetCore.Http;
using OnlineGameStore.Application.Auth;

namespace OnlineGameStore.Application.Middleware
{
    public class CurrentUserMiddleware : IMiddleware
    {
        private readonly ICurrentUser _currentUser;

        public CurrentUserMiddleware(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _currentUser.SetUser(context.User);
            await next(context);
        }
    }
}
