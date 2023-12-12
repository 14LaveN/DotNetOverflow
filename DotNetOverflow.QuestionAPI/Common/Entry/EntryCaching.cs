namespace DotNetOverflow.QuestionAPI.Common.Entry;

public static class EntryCaching
{
    public static IServiceCollection AddCachingEntry(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddResponseCaching(options =>
        {
            options.UseCaseSensitivePaths = false; 
            options.MaximumBodySize = 1024; 
        });
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "Questions_";
        });

        services.AddDistributedMemoryCache();
        
        return services;
    }
}