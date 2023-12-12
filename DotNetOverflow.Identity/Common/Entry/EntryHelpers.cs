using DotNetOverflow.Core.Helpers.Metric;

namespace DotNetOverflow.Identity.Common.Entry;

public static class EntryHelpers
{
    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<CreateMetricsHelper>();
        
        return services;
    }
}