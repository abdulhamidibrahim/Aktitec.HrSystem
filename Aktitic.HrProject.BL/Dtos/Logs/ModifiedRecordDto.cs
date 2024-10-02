namespace Aktitic.HrProject.BL;

public class ModifiedRecordDto
{
    public int Id { get; set; }
    public int AuditLogId { get; set; }
    public string? RecordId { get; set; }
    public bool PermenantlyDeleted { get; set; }
    public string? PermenantlyDeletedBy { get; set; }
}