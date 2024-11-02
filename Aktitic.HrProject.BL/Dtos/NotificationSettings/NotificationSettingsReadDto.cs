using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class NotificationSettingsReadDto
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public int? CompanyId { get; set; }
    public string PageCode { get; set; }
}
