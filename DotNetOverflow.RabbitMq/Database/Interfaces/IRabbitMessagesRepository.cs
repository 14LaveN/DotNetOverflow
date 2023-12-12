using DotNetOverflow.Core.Entity.Message;

namespace DotNetOverflow.RabbitMq.Database.Interfaces;

public interface IRabbitMessagesRepository
{
    Task<List<RabbitMessage>> GetAllAsync();

    Task<RabbitMessage?> GetAsync(string id);

    Task CreateAsync(RabbitMessage newProfile);

    Task UpdateAsync(string id, RabbitMessage updatedProfile);

    Task RemoveAsync(string id);
}