using System.Security.Claims;

namespace OnlineGameStore.Application.Auth
{
    public interface ICurrentUser
    {
        void SetUser(ClaimsPrincipal user);

        string GetUserId();
    }
}
