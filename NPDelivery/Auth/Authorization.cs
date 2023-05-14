using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace NPDelivery.Auth;

public class NPAuthorizationService : IAuthorizationService
{
    private readonly ILogger<NPAuthorizationService> _logger;

    public NPAuthorizationService(ILogger<NPAuthorizationService> logger)
    {
        _logger = logger;
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        return Authorize(user, resource);
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, string policyName)
    {
        return Authorize(user, resource);
    }

    private Task<AuthorizationResult> Authorize(ClaimsPrincipal user, object? resource)
    {
        var identity = user.Identity;
        _logger.LogInformation("Authorization attempt from user {Username} for resource {@Resource}", identity.Name, resource);
        if (!identity.IsAuthenticated)
        {
            return Task.FromResult(AuthorizationResult.Failed());
        }
        return Task.FromResult(AuthorizationResult.Success());
    }
}
