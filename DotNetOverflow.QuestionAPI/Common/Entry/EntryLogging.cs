using NLog.Web;

namespace DotNetOverflow.QuestionAPI.Common.Entry;

public static class EntryLogging
{
    public static IServiceCollection AddLogs(this IServiceCollection services,
        ConfigureHostBuilder host)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddNLogWeb("nlogQuestion.config");
        });

        host.UseNLog();
        
        return services;
    }
}