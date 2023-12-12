using System.Diagnostics;
using System.Security.Claims;
using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Core.Helpers.Metric;
using DotNetOverflow.Identity.Commands.Login;
using DotNetOverflow.Identity.Commands.Register;
using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using DotNetOverflow.Identity.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DotNetOverflow.Identity.Controllers;

[Route("api/v1/identity")]
public class IdentityController(AppDbContext context,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        CreateMetricsHelper createMetricsHelper)
    : ApiBaseController
{
    /// <summary>
    /// Login account
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Base information about register an account</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return description and access token</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPost("login-user")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand request)
    {
        var stopWatch = Stopwatch.StartNew();
        
        var response = await mediator.Send(request);
        
        stopWatch.Stop();
        await createMetricsHelper.CreateMetrics(stopWatch);
        
        if (response.StatusCode == Core.Enum.StatusCodes.StatusCode.Ok)
        {
            return Ok(new
            {
                description = response.Description,
                accessToken = response.AccessToken
            });
        }
        return BadRequest(new { descritpion = response.Description});
    }
    
    /// <summary>
    /// Register account
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Base information about login an account</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return description and access token</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///

    [HttpPost("register-user")]
    public async Task<AppUser> Register(
        [FromBody] RegisterCommand request)
    {
        var stopWatch = Stopwatch.StartNew();
        
        var response = await mediator.Send(request);
        
        stopWatch.Stop();
        await createMetricsHelper.CreateMetrics(stopWatch);
        
        if (response.StatusCode == Core.Enum.StatusCodes.StatusCode.Ok)
        {
            return response.Data!;
        }

        throw new AggregateException(response.Description);
    }

    [HttpGet("get-profile-for-otherServices")]
    public async Task<AppUser> GetProfileForOtherServices()
    {
        var name = GetProfile();
        var user = await unitOfWork.AppUserRepository.GetByName(name!);
        return user;
    }
    
    [HttpGet("get-profile")]
    public string? GetProfile()
    {
        var name = User.Claims.FirstOrDefault(x=> x.Type == "name")?.Value;
        return name;
    }
}