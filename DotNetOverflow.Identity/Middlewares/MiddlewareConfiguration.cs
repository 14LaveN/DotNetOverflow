namespace DotNetOverflow.Identity.Middlewares;

public static class MiddlewareConfiguration
{
    public static WebApplication UseCustomMiddlewares(this WebApplication app)
    {
        if (app is null)
            throw new ArgumentException();
        
        app.UseMiddleware<RequestLoggingMiddleware>(app.Logger);
        //TODO app.UseMiddleware<ResponseCachingMiddleware>();
       //TODO app.UseMiddleware<ExceptionMiddleware>(app.Logger);// - Exceptions Middleware

        return app;
    }
}