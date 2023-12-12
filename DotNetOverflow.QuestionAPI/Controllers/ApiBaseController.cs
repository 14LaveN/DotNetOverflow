using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Core.Helpers.JWT;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetOverflow.QuestionAPI.Controllers;

[ApiController]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "v1")]
public class ApiBaseController(IUnitOfWork unitOfWork) : Controller
{
    protected string? Token =>
        Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    
    /// <summary>
    /// Get name
    /// </summary>
    /// <returns>Base information about get name method</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return name by token</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    /// 
    [HttpGet("get-name")]
    public string GetName()
    {
        var name = GetClaimByJwtToken.GetNameByToken(Token);
        return name;
    }

    /// <summary>
    /// Get profile
    /// </summary>
    /// <returns>Base information about get pfoile method</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return app user</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpGet("get-profile")]
    public async Task<AppUser> GetProfile()
    {
        var name = GetName();
        var profile = await unitOfWork
            .AppUserRepository
            .GetByName(name);

        return profile;
    }

    [HttpGet("get-profile-by-id")]
    public async Task<AppUser> GetProfileById(long authorId)
    {
        var profile = await unitOfWork
            .AppUserRepository
            .GetById(authorId);

        return profile;
    }
}