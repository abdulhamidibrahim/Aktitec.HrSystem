namespace Aktitic.HrProject.DAL.Models;

public class ModifiedRecord
{
    public int Id { get; set; }
    public int AuditLogId { get; set; }
    public AuditLog AuditLog { get; set; }
    public string? RecordId { get; set; }
    public bool PermenantlyDeleted { get; set; }
    public string? PermenantlyDeletedBy { get; set; }
}