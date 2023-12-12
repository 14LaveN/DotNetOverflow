using System.Text;
using DotNetOverflow.RabbitMq.Interfaces;
using RabbitMQ.Client;

namespace DotNetOverflow.RabbitMq.Implementations;

public class RabbitMqService : IRabbitMqService
{
    public async Task SendMessage(string message,
        string rabbitName)
    {
        var factory = new ConnectionFactory() { Uri = new Uri("amqps://dgpswpjt:tbQvnOh93n-sdqDMjXAjfB53OiShmOka@chimpanzee.rmq.cloudamqp.com/dgpswpjt") };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: $"{rabbitName}-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        await channel.QueueBindAsync(queue: $"{rabbitName}-queue", exchange: $"{rabbitName}-exchange", routingKey: $"{rabbitName}-queue");
        
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: $"{rabbitName}-exchange",
            routingKey: $"{rabbitName}-queue",
            basicProperties: new BasicProperties(),
            body: body);
    }
}