using DotNetOverflow.ImageAPI.DAL;

namespace DotNetOverflow.ImageAPI.Common.Entry;

public static class EntryDatabase
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddImageDatabase(configuration);
        
        return services;
    }
}