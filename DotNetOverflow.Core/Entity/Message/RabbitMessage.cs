namespace DotNetOverflow.Core.Entity.Message;

public class RabbitMessage : BaseEntity
{
    public required string Description { get; set; }

    public static implicit operator RabbitMessage(string description)
    {
        return new RabbitMessage()
        {
            Description = description
        };
    }
}