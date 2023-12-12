using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace DotNetOverflow.Identity.Middlewares;

public class ResponseCachingMiddleware(RequestDelegate next,
    IDistributedCache cache)
{
    public async Task Invoke(HttpContext context)
    {
        var cacheKey = context.Request.Path;
        var cachedResponse = await cache.GetStringAsync(cacheKey);

        if (!cachedResponse.IsNullOrEmpty())
        {
            await context.Response.WriteAsync(cachedResponse!);
            return;
        }

        var originalResponseBody = context.Response.Body;
        using var memStream = new MemoryStream();
        context.Response.Body = memStream;

        await next(context);

        memStream.Position = 0;
        var responseBody = await new StreamReader(memStream).ReadToEndAsync();

        await cache.SetStringAsync(cacheKey, responseBody);

        memStream.Position = 0;
        await memStream.CopyToAsync(originalResponseBody);
    }
}