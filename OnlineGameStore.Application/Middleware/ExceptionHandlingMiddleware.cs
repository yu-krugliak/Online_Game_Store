using Microsoft.AspNetCore.Http;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Models;
using System.Net;

namespace OnlineGameStore.Application.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }

            catch (Exception ex)
            {
                var errorModel = new ErrorInfoModel()
                {
                    Source = ex.TargetSite?.DeclaringType?.FullName,
                    Exception = ex.Message.Trim(),
                    StackTrace = ex.StackTrace
                };
                errorModel.Messages.Add(ex.Message);

                if (ex is not CustomException && ex.InnerException != null)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        errorModel.Messages.Add(ex.Message);
                    }
                }

                switch (ex)
                {
                    case CustomException ce:
                        context.Response.StatusCode = (int)ce.StatusCode;

                        if (ce.ErrorMessages is not null)
                            errorModel.Messages = ce.ErrorMessages;

                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                if (context.Response.HasStarted)
                {
                    // TODO: Log Error
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(errorModel);
            }
        }
    }
}
