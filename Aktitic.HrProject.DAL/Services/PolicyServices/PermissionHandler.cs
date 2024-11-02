using System.Security.Claims;
using Aktitic.HrProject.DAL.Context;
using Microsoft.AspNetCore.Authorization;

namespace Aktitic.HrProject.DAL.Services.PolicyServices;

public class PermissionHandler(HrSystemDbContext dbContext) : AuthorizationHandler<PermissionRequirement>
{
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Task.CompletedTask;
        }
        
        var user = dbContext.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(userId));
        if(user != null && (user.IsAdmin || user.IsManager))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (user != null)
        {
            var userRole = dbContext.CompanyRoles?.Find(user.RoleId);
            if (userRole == null)
            {
                return Task.CompletedTask;
            }

            var permissions = dbContext.RolePermissions?
                .FirstOrDefault(rp => rp.CompanyRoleId == userRole.Id && rp.PageCode == requirement.PageCode);

            if (permissions == null)
            {
                return Task.CompletedTask;
            }

       
            var hasPermission = requirement.PermissionType switch
            {
                "Read" => permissions.Read,
                "Add" => permissions.Add,
                "Edit" => permissions.Edit,
                "Delete" => permissions.Delete,
                "Export" => permissions.Export,
                "Import" => permissions.Import,
                _ => false
            };

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}