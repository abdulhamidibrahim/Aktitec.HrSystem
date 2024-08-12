using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class ScheduleTimingsUpdateDto
{
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public DateTime ScheduleDate1 { get; set; }
    public DateTime ScheduleDate2 { get; set; }
    public DateTime ScheduleDate3 { get; set; }
    public string? SelectTime1 { get; set; }
    public string? SelectTime2 { get; set; }
    public string? SelectTime3 { get; set; }

}
