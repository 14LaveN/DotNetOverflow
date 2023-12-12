using DotNetOverflow.Identity.DAL.Database.HealthCheck;
using DotNetOverflow.Metrics.DAL;
using DotNetOverflow.RabbitMq.Implementations;

namespace DotNetOverflow.ImageAPI.Common.Entry;

public static class EntryHealthChecks
{
    public static IServiceCollection AddHealthChecksEntry(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddHealthChecks()
            .AddCheck<GeneralDbHealthCheck>(nameof(GeneralDbHealthCheck))
            .AddCheck<RabbitMqHealthCheck>(nameof(RabbitMqHealthCheck))
            .AddCheck<MongoHealthCheck>(nameof(MongoHealthCheck));
        
        return services;
    }
}