using DotNetOverflow.Core.Entity.Metrics;
using DotNetOverflow.Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetOverflow.Metrics.DAL;

public class MetricsDbContext
{
    private readonly IMongoDatabase _database = null!;

    public MetricsDbContext(IOptions<MongoSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client != null)
            _database = client.GetDatabase(settings.Value.Database);
    }
    
    public IMongoCollection<MetricEntity> Metrics =>
        _database.GetCollection<MetricEntity>("Metrics");
}