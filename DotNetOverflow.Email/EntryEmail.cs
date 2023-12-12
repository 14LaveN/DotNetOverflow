using DotNetOverflow.Email.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetOverflow.Email;

public static class EntryEmail
{
    public static IServiceCollection AddEmailService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<IEmailService, EmailService>();
        //TODO services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        
        return services;
    }
}