using Aktitic.HrProject.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aktitic.HrProject.DAL.Services.PolicyServices;


public class AuthorizeRoleAttribute(string pageCode, string permissionType) : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContextAccessor = context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
        var authorizationService = context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService)) as IAuthorizationService;
        var dbContext = context.HttpContext.RequestServices.GetService(typeof(HrSystemDbContext)) as HrSystemDbContext;

        if (httpContextAccessor == null || authorizationService == null || dbContext == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var user = httpContextAccessor.HttpContext.User;
        var permissionRequirement = new PermissionRequirement(permissionType)
        {
            PageCode = pageCode
        };

        var result = await authorizationService.AuthorizeAsync(user, new PermissionHandler(dbContext), permissionRequirement);

        if (!result.Succeeded)
        {
            context.Result = new ForbidResult();
            return;
        }

        // Continue to the action
        await next();
    }
}