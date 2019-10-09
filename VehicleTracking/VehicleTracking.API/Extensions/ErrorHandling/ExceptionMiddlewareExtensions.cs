using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using VehicleTracking.Common.Exceptions;

namespace VehicleTracking.Api.Extensions.ErrorHandling
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
                        string statusCode = Convert.ToString((int)HttpStatusCode.InternalServerError);
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
