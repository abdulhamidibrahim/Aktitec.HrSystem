using Microsoft.AspNetCore.Authorization;

namespace Aktitic.HrProject.DAL.Services.PolicyServices;

public class PermissionRequirement(string permissionType, string pageCode) : IAuthorizationRequirement
{
    public string PermissionType { get; } = permissionType;
    public string PageCode { get; } = pageCode;
}