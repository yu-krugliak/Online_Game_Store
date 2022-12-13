using System.Net;

namespace OnlineGameStore.Application.Exceptions;
public class InvalidRequestException : CustomException
{
    public InvalidRequestException(string message)
        : base(message, null, HttpStatusCode.NotAcceptable)
    {
    }
}