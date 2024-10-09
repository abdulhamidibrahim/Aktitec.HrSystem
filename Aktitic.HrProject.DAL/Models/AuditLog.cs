using System;

namespace Aktitic.HrProject.DAL.Models;

public class AuditLog : IMustHaveTenant
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public string UserId { get; set; }
    public LogAction? Action { get; set; }
    public int? ActionId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string? Changes { get; set; }
    public string IpAddress { get; set; }
    public AppPages? AppPages { get; set; }
    public int? AppPagesId { get; set; }
    public ICollection<ModifiedRecord>? ModifiedRecords { get; set; }
    public int? TenantId { get; set; }
}