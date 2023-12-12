namespace DotNetOverflow.Core.Exception;

public class NotFoundException : System.Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) not found.") { }
}