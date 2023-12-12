namespace DotNetOverflow.Core.Entity.Metrics;

public class MetricEntity : BaseEntity
{
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    //TODO public static implicit operator MetricEntity(string description)
    //TODO {
    //TODO     return new MetricEntity()
    //TODO     {
    //TODO         Description = description
    //TODO     };
    //TODO }
}