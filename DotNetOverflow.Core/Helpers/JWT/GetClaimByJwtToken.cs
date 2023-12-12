using System.IdentityModel.Tokens.Jwt;

namespace DotNetOverflow.Core.Helpers.JWT;

public static class GetClaimByJwtToken
{
    public static string GetNameByToken(string? token)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken tokenInfo = handler.ReadJwtToken(token);
        
        var claimsPrincipal = tokenInfo.Claims;
        
        var name = claimsPrincipal.FirstOrDefault(x=> x.Type == "name")?.Value;
        return name!;
    }
}