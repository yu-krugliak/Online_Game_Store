using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
