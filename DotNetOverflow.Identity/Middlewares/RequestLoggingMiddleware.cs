namespace DotNetOverflow.Identity.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly ILogger _logger;
    private RequestDelegate _next;
    
    public RequestLoggingMiddleware(ILogger logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Записать запрос в журнал
        _logger.LogInformation("Get request: {Request}", context.Request);

        await _next.Invoke(context);
    }
}