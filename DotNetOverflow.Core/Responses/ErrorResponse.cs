namespace DotNetOverflow.Core.Responses;

public class ErrorResponse
{
    public required string Message { get; set; }
    
    public required string ErrorCode { get; set; }
}