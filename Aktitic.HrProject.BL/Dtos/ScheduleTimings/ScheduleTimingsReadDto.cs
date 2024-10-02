using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ScheduleTimingsReadDto
{
    public int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required int JobId { get; set; }
    public DateTime? ScheduleDate1 { get; set; }
    public DateTime? ScheduleDate2 { get; set; }
    public DateTime? ScheduleDate3 { get; set; }
    public string? SelectTime1 { get; set; }
    public string? SelectTime2 { get; set; }
    public string? SelectTime3 { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
