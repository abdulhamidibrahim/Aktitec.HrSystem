using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class LogsReadDto
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public string UserId { get; set; }
    public string Action { get; set; }
    public DateTime TimeStamp { get; set; }
    public string? Changes { get; set; }
    public string IpAddress { get; set; }
    public ICollection<ModifiedRecord>? ModifiedRecords { get; set; }
    public int? TenantId { get; set; }
}
