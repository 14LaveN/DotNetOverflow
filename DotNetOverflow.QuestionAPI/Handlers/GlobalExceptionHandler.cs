using System.Net;
using System.Text.Json;
using Amazon.Runtime;
using DotNetOverflow.Core.Responses;

namespace DotNetOverflow.QuestionAPI.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context,
        Exception exception)
    {
        logger.LogError(exception, "An unhandled exception occurred.");

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Message = "An error occurred. Please try again later.",
            ErrorCode = "500"
        };

        var json = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(json);

        return true;
    }

    public bool Handle(IExecutionContext executionContext,
        Exception exception)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HandleAsync(IExecutionContext executionContext,
        Exception exception)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred.");

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Message = "An error occurred. Please try again later.",
            ErrorCode = "500"
        };

        var json = JsonSerializer.Serialize(errorResponse);
        await httpContext.Response.WriteAsJsonAsync(json, cancellationToken);

        return true;
    }
}