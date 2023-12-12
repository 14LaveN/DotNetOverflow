using Microsoft.AspNetCore.Authorization;

namespace DotNetOverflow.Identity;

public class RequireScopeHandler : AuthorizationHandler<ScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
    {
        var scopeClaim = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);

        if (scopeClaim == null || string.IsNullOrWhiteSpace(scopeClaim.Value))
        {
            context.Fail(new AuthorizationFailureReason(this, "Scopes was null"));
            return Task.CompletedTask;
        }

        if (scopeClaim.Value.Split(' ').Any(s => s == requirement.Scope))
            context.Succeed(requirement);
        else
            context.Fail(new AuthorizationFailureReason(this, "Scope invalid"));
        
        return Task.CompletedTask;
    }
}

public class ScopeRequirement(string issuer, string scope) : IAuthorizationRequirement
{
    public string Issuer { get; } = issuer ?? throw new ArgumentNullException(nameof(issuer));
    public string Scope { get; } = scope ?? throw new ArgumentNullException(nameof(scope));
}