using DotNetOverflow.Identity.DAL.Database.HealthCheck;
using DotNetOverflow.RabbitMq.Implementations;

namespace DotNetOverflow.Identity.Common.Entry;

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
            .AddCheck<RabbitMqHealthCheck>(nameof(RabbitMqHealthCheck));
            //TODO .AddCheck<MongoHealthCheck>(nameof(MongoHealthCheck));
        
        return services;
    }
}