using DotNetOverflow.RabbitMq.Implementations;
using DotNetOverflow.RabbitMq.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetOverflow.RabbitMq;

public static class EntryRabbitMq
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services,
        string rabbitName)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddHostedService<AbstractConsumer>(provider => new AbstractConsumer(rabbitName));
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        
        return services; 
    }
}