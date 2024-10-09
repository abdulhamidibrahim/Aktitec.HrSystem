using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.DAL.Services.PolicyServices;


public class AuthorizationHelperService(
    IAuthorizationService authorizationService,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task<bool> CheckPermissionAsync(string permissionType, string pageCode)
    {
        var user = httpContextAccessor.HttpContext.User;
        var permissionRequirement = new PermissionRequirement(permissionType, pageCode);

        var result = await authorizationService.AuthorizeAsync(user, null, permissionRequirement);
        return result.Succeeded;
    }
}