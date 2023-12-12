using System.Data.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace DotNetOverflow.RabbitMq.Implementations;

public class RabbitMqHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory { Uri = new Uri("amqps://dgpswpjt:tbQvnOh93n-sdqDMjXAjfB53OiShmOka@chimpanzee.rmq.cloudamqp.com/dgpswpjt")};
        try
        {
            using var connection = await factory.CreateConnectionAsync();
        }
        catch (DbException ex)
        {
            return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
        }

        return HealthCheckResult.Healthy();
    }
}