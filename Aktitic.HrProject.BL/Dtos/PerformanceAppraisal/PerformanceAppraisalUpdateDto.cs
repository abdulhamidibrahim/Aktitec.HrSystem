using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class PerformanceAppraisalUpdateDto
{
    public int? EmployeeId { get; set; }
    public DateOnly? Date { get; set; }
    public bool? Status { get; set; }
}
