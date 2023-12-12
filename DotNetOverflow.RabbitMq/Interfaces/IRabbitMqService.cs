namespace DotNetOverflow.RabbitMq.Interfaces;

public interface IRabbitMqService
{
    Task SendMessage(string message, string rabbitName);
}