using Microsoft.AspNetCore.Authorization;

namespace FireBranchDev.MyLibrary.Api;

public class ScopeRequirementHandler : AuthorizationHandler<ScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer)) return Task.CompletedTask;

        var claim = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);
        if (claim is null) return Task.CompletedTask;

        var scopes = claim.Value.Split(" ");
        if (scopes.Any(s => s == requirement.Scope)) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
