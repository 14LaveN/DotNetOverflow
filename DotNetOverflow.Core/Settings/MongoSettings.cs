namespace DotNetOverflow.Core.Settings;

public class MongoSettings
{
    public string ConnectionString { get; set; } = null!;

    public string Database { get; set; } = null!;

    public string RabbitMessagesCollectionName { get; set; } = null!;

    public string MetricsCollectionName { get; set; } = null!;
    
    public string LikesCollectionName { get; set; } = null!;
    
    public string CommentsCollectionName { get; set; } = null!;
}