using Leftovers.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Leftovers.Auth
{
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirment, IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirment requirement, IUserOwnedResource resource)
        {
            if (context.User.IsInRole(LeftoversUserRoles.Admin) || context.User.FindFirst(CustomClaims.UserId).Value == resource.UserId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public record SameUserRequirment : IAuthorizationRequirement;
}
