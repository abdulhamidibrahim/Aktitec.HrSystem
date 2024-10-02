using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class PolicyDto
{
    public int Id  { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? DepartmentId { get; set; }
    public string? Department { get; set; }
    public DateOnly? Date { get; set; }
    public string? File { get; set; }
    public string? FileName { get; set; }
    public byte[]? FileContent { get; set; }}