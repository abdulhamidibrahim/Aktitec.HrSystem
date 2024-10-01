using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class ResignationReadDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string? Reason { get; set; }
    public DateOnly? NoticeDate { get; set; }
    public DateOnly? ResignationDate { get; set; }
    public EmployeeDto? Employee { get; set; }
}
