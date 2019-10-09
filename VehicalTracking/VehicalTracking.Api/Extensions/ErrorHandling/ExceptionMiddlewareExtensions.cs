using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using VehicalTracking.Common.Exceptions;

namespace VehicalTracking.Api.Extensions.ErrorHandling
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    if (contextFeature != null)
                    {
                        string statusCode = HttpStatusCode.InternalServerError.ToString();
                        string message = contextFeature.Error.Message ?? "Internal Server Error";

                        if (contextFeature.Error.GetType() == typeof(CustomException))
                        {
                            var customException = (CustomException)contextFeature.Error;

                            statusCode = customException.ErrorCode;
                            message = customException.ErrorMessage;
                        }

                        await context.Response.WriteAsync(new ErrorResponse()
                        {
                            StatusCode = statusCode,
                            Message = message,
                            StackTrace = contextFeature.Error.StackTrace
                        }.ToString());
                    }
                });
            });
        }
    }
}
