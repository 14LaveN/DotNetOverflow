using DotNetOverflow.ImageAPI.Commands.Image.CreateImage;

namespace DotNetOverflow.ImageAPI.Common.Entry;

public static class EntryMediatr
{public static IServiceCollection AddMediatrExtension(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(typeof(CreateImageCommand).Assembly,
                typeof(CreateImageCommandHandler).Assembly);
        });
        
        return services;
    }
    
}