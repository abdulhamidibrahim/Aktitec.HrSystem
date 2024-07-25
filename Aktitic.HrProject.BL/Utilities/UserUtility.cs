using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL.Utilities;

public class UserUtility
{
    private readonly HttpContextAccessor _contextAccessor;

    public UserUtility(HttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    
    
    public string GetUserId() => _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // ClaimTypes.NameIdentifier
    public string GetUserName() => _contextAccessor.HttpContext.User.FindFirstValue(claimType: ClaimTypes.Name);
}