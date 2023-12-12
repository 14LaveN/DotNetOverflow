using System.Data.Common;
using DotNetOverflow.Core.Settings;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetOverflow.Metrics.DAL;

public class MongoHealthCheck
    : IHealthCheck
{
    private readonly IMongoDatabase _database = null!;

    public MongoHealthCheck(IOptions<MongoSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client is not null)
            _database = client.GetDatabase(settings.Value.Database);
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _database.Client.ListDatabasesAsync(cancellationToken);
        }
        catch (DbException ex)
        {
            return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
        }

        return HealthCheckResult.Healthy();
    }
}