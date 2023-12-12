using System.Diagnostics;
using System.Globalization;
using DotNetOverflow.Core.Entity.Metrics;
using DotNetOverflow.Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Prometheus;

namespace DotNetOverflow.Core.Helpers.Metric;

public class CreateMetricsHelper
{
    public static readonly Counter RequestCounter = Metrics.CreateCounter("dotnetoverflow_requests_total", "Total number of requests.");
    private readonly IMongoCollection<MetricEntity> _metricsCollection;

    public CreateMetricsHelper(IOptions<MongoSettings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        _metricsCollection = mongoDatabase.GetCollection<MetricEntity>(
            dbSettings.Value.MetricsCollectionName);
    }

    public async Task CreateMetrics(Stopwatch stopwatch)
    {
        RequestCounter.Inc();
        
        Metrics.CreateHistogram("dotnetoverflow_request_duration_seconds", "Request duration in seconds.")
            .Observe(stopwatch.Elapsed.TotalMilliseconds);

        var metrics = new List<MetricEntity>()
        { 
            new()
            {
                Name = "dotnetoverflow_request_duration_seconds", 
                Description = stopwatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.CurrentCulture)
            },
            new()
            {
                Name = RequestCounter.Name,
                Description = RequestCounter.Value.ToString(CultureInfo.CurrentCulture)
            }
        };
            
        await _metricsCollection.InsertManyAsync(metrics);
    }
}