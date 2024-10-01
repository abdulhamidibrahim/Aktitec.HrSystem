using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class PoliciesReadDto
{
    public int Id  { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? DepartmentId { get; set; }
    public DepartmentDto? Department { get; set; }
    public DateOnly? Date { get; set; }
    public string? File { get; set; }
}
