using System.Diagnostics;
using System.Text;
using DotNetOverflow.Core.Entity.Message;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ConnectionFactory = RabbitMQ.Client.ConnectionFactory;
using ExchangeType = RabbitMQ.Client.ExchangeType;
using IChannel = RabbitMQ.Client.IChannel;
using IConnection = RabbitMQ.Client.IConnection;

namespace DotNetOverflow.RabbitMq.Implementations;

public class AbstractConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private static IChannel _channel;
    private static string _rabbitName;
    private readonly IMongoCollection<RabbitMessage> _rabbitMessagesCollection;

    public AbstractConsumer(string rabbitName)
    {
        var mongoClient = new MongoClient(
            "mongodb://localhost:27017");

        var mongoDatabase = mongoClient.GetDatabase(
            "DNO");

        _rabbitMessagesCollection = mongoDatabase.GetCollection<RabbitMessage>(
            "RabbitMessages");
        _rabbitName = rabbitName;
        var factory = new ConnectionFactory { Uri = new Uri("amqps://dgpswpjt:tbQvnOh93n-sdqDMjXAjfB53OiShmOka@chimpanzee.rmq.cloudamqp.com/dgpswpjt")};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateChannel();
        _channel.ExchangeDeclare(exchange: $"{rabbitName}-exchange", type: ExchangeType.Topic, durable: true);
        _channel.QueueDeclare(queue: $"{rabbitName}-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: $"{rabbitName}-queue", exchange: $"{rabbitName}-exchange", routingKey: $"{rabbitName}-queue");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			
            _rabbitMessagesCollection.InsertOne(content, cancellationToken: stoppingToken);
            
            // Каким-то образом обрабатываем полученное сообщение
            Console.WriteLine(content);
            Debug.WriteLine($"Message recieved: {content}");

            await _channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await _channel.BasicConsumeAsync($"{_rabbitName}-queue", false, consumer);
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}