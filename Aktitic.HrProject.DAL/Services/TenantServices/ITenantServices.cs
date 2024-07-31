using Aktitic.HrProject.DAL.Settings;

namespace Aktitic.HrProject.DAL.Services.TenantServices;

public interface ITenantServices
{
    Tenant? GetCurrentTenant();
    string? GetDatabaseProvider();
    string? GetConnectionString();
    
}