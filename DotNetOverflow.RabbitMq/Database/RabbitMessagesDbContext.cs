using DotNetOverflow.Core.Entity.Message;
using DotNetOverflow.Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetOverflow.RabbitMq.Database;

public class RabbitMessagesDbContext
{
    private readonly IMongoDatabase database = null!;

    public RabbitMessagesDbContext(IOptions<MongoSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client != null)
            database = client.GetDatabase(settings.Value.Database);
    }
    public IMongoCollection<RabbitMessage> RabbitMessages =>
        database.GetCollection<RabbitMessage>("RabbitMessages");
}