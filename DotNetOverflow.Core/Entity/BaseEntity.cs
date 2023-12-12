using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetOverflow.Core.Entity;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}