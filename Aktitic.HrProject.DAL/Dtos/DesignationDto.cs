using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class DesignationDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    
    public int? DepartmentId { get; set; }
    
}