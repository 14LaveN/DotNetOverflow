using DotNetOverflow.Core.Helpers.Metric;

namespace DotNetOverflow.QuestionAPI.Common.Entry;

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