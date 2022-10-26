using System.Net;

namespace OnlineGameStore.Application.Exceptions;
public class UnauthorizedException : CustomException
{
    public UnauthorizedException(string message)
        : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}