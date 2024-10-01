using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL;

public class LogsDto
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public string UserId { get; set; }
    public LogActionDto Action { get; set; }
    public DateTime TimeStamp { get; set; }
    public string? Changes { get; set; }
    public string IpAddress { get; set; }
    public AppPagesDto? AppPages { get; set; }
    public ICollection<ModifiedRecordDto>? ModifiedRecords { get; set; }
    public int? TenantId { get; set; }
}