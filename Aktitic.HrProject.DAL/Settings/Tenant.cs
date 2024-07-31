namespace Aktitic.HrProject.DAL.Settings;

public class Tenant
{
    public int TId { get; set; } 
    public string CompanyName { get; set; } = null!;
    public string? ConnectionString { get; set; } 
    
}