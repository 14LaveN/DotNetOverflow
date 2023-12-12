using DotNetOverflow.Identity.Commands.Login;
using DotNetOverflow.Identity.Commands.Register;
using FluentValidation;

namespace DotNetOverflow.Identity.Common.Entry;

public static class EntryValidator
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        
        return services;
    }
}