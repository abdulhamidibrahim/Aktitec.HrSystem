using System.Security.Claims;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL.Utilities;

public class UserUtility
{
    private static readonly HttpContextAccessor? _contextAccessor;

    // protected static UserUtility(HttpContextAccessor contextAccessor)
    // {
    //     _contextAccessor = contextAccessor;
    // }
    
    
    public static string GetUserId() => _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // ClaimTypes.NameIdentifier
    public static string GetUserName() => _contextAccessor.HttpContext.User.FindFirstValue(claimType: ClaimTypes.Name);
    
    public static string GetCurrentCompany() => _contextAccessor.HttpContext.User.FindFirstValue(nameof(Company));
}