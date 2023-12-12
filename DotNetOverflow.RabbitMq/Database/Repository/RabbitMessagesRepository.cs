using DotNetOverflow.Core.Entity.Message;
using DotNetOverflow.Core.Settings;
using DotNetOverflow.RabbitMq.Database.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetOverflow.RabbitMq.Database.Repository;

public class RabbitMessagesRepository : IRabbitMessagesRepository
{
    private readonly IMongoCollection<RabbitMessage> _rabbitMessagesCollection;

    public RabbitMessagesRepository(
        IOptions<MongoSettings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        _rabbitMessagesCollection = mongoDatabase.GetCollection<RabbitMessage>(
            dbSettings.Value.RabbitMessagesCollectionName);
    }
    
    public async Task<List<RabbitMessage>> GetAllAsync() =>
        await _rabbitMessagesCollection.Find(_ => true).ToListAsync();

    public async Task<RabbitMessage?> GetAsync(string id) =>
        await _rabbitMessagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(RabbitMessage newArticle) =>
        await _rabbitMessagesCollection.InsertOneAsync(newArticle);

    public async Task UpdateAsync(string id, RabbitMessage updatedArticle) =>
        await _rabbitMessagesCollection.ReplaceOneAsync(x => x.Id == id, updatedArticle);

    public async Task RemoveAsync(string id) =>
        await _rabbitMessagesCollection.DeleteOneAsync(x => x.Id == id);
}