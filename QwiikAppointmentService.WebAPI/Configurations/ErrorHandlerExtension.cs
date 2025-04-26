using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.IdentityModel.Tokens;
using QwiikAppointmentService.Application.Exceptions;

namespace Evercare.HealthrecordService.WebApi.Configurations;

public static class ErrorHandlerExtensions
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
            var loggingFactory = serviceScope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggingFactory?.CreateLogger<IApplicationBuilder>();
            var requestMethodList = new string[] { "POST", "PUT", "PATCH" };
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                var processId = Guid.NewGuid().ToString();
                try
                {
                    logger?.LogError($"processId:{processId} - error message: {contextFeature.Error.GetBaseException().Message}, request path: {context.Request.Path}");
                    if (requestMethodList.Contains(context.Request.Method))
                    {
                        using (var bodyStream = new StreamReader(context.Request.Body))
                        {
                            if (context.Request.Body.CanSeek) context.Request.Body.Seek(0, SeekOrigin.Begin);
                            var bodyText = await bodyStream.ReadToEndAsync();
                            logger?.LogError($"processId:{processId} - error message: {contextFeature.Error.GetBaseException().Message}, request body: {bodyText}");
                        }
                    }
                    logger?.LogError(contextFeature.Error, $"processId:{processId} - error message: {contextFeature.Error.GetBaseException().Message}, detail: ");
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, $"processId:{processId} - Cannot extract detail error: {ex.Message}");
                }

                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";

                if (contextFeature.Error is BadRequestException badRequestException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var badRequestResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = contextFeature.Error.GetBaseException().Message,
                        errors = badRequestException.Errors
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(badRequestResponse));
                    return;
                }

                context.Response.StatusCode = contextFeature.Error switch
                {
                    JsonPatchException => (int)HttpStatusCode.BadRequest,
                    OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorisedException => (int)HttpStatusCode.Unauthorized,
                    SecurityTokenException => (int)HttpStatusCode.Unauthorized,
                    UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                    InternalServerErrorException => (int)HttpStatusCode.InternalServerError,
                    //ForbiddenException => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = contextFeature.Error.GetBaseException().Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            });
        });
    }
}