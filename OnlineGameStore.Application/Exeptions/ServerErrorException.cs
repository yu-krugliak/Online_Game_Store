using System.Net;

namespace OnlineGameStore.Application.Exeptions
{
    public class ServerErrorException : CustomException
    {
        public ServerErrorException(string message, List<string>? errorMessages)
            : base(message, errorMessages, HttpStatusCode.InternalServerError)
        {
        }
    }
}