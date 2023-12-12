using DotNetOverflow.Core.Enum.StatusCodes;

namespace DotNetOverflow.Identity.Models.Identity;

public class LoginResponse<T> 
    where T : class
{
    public required string Description { get; set; } = null!;

    public T? Data { get; set; }

    public required StatusCode StatusCode { get; set; } = StatusCode.Ok;
    
    public string AccessToken { get; set; } = null!;
    
    public string RefreshToken { get; set; } = null!;
    
    public DateTime RefreshTokenExpireAt { get; set; }
}