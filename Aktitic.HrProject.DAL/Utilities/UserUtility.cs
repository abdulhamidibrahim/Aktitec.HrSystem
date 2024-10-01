using System.Security.Claims;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL.Utilities;

public class UserUtility(HttpContextAccessor contextAccessor)
{
    private readonly HttpContextAccessor? _contextAccessor = contextAccessor;


    public string GetUserId() => _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); 
    public string GetUserName() => _contextAccessor.HttpContext.User.FindFirstValue(claimType: ClaimTypes.Name);
    
    public string? GetCurrentCompany() => _contextAccessor?.HttpContext?.User.FindFirst(nameof(Company))?.Value;

    public string GetIpAddress()
    {
        var ip = _contextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString();
        return ip;
    }
    
    public string GetCurrentPage() => _contextAccessor?.HttpContext?.Request.Path.Value;
}