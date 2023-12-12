using DotNetOverflow.ImageAPI.Commands.Image.CreateImage;
using FluentValidation;

namespace DotNetOverflow.ImageAPI.Common.Entry;

public static class EntryValidator
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<IValidator<CreateImageCommand>, CreateImageCommandValidator>();
        
        return services;
    }
}