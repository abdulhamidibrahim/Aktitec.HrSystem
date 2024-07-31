using Aktitic.HrProject.DAL.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aktitic.HrProject.DAL.Services.TenantServices;

public class TenantServices : ITenantServices
{
    private HttpContext? _httpContext;
    private Tenant? _currentTenant;
    private readonly TenantSettings _tenantSettings;

    public TenantServices(IHttpContextAccessor contextAccessor, IOptions<TenantSettings> tenantSettings)
    {
        _tenantSettings = tenantSettings.Value;
        _httpContext = contextAccessor.HttpContext;

        if (_httpContext is not null)
        {
            if (_httpContext.Request.Headers.TryGetValue("X-Tenant", out var tenantId))
            {
                var tenantIdInt = int.TryParse(tenantId,out var tenantIdIntValue) 
                    ? tenantIdIntValue 
                    : throw new Exception("Invalid tenant id");
                SetCurrentTenant(tenantIdInt);
            }
            else
            {
                throw new Exception("X-Tenant header is missing");
            }
        }
    }

    public Tenant? GetCurrentTenant()
    {
        return _currentTenant;
    }

    public string? GetDatabaseProvider()
    {
        return _tenantSettings.Defaults.DbProvider;
    }

    public string? GetConnectionString()
    {
        var connectionString = _currentTenant is null
            ? _tenantSettings.Defaults.ConnectionString
            : _currentTenant.ConnectionString;
        return connectionString;
    }

    private void SetCurrentTenant(int tenantId)
    {
        _currentTenant = _tenantSettings.Tenants.FirstOrDefault(x=> x.TId == tenantId);
        if (_currentTenant is null)
        {
            throw new Exception("Invalid tenant id");
        }

        if (_currentTenant.ConnectionString.IsNullOrEmpty())
        {
            _currentTenant.ConnectionString = _tenantSettings.Defaults.ConnectionString;
        }
    }
}