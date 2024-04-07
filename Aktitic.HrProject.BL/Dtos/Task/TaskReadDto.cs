using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class TaskReadDto
{
    public int Id { get; set; }
    
    public string Text { get; set; }=string.Empty;
    
    public string? Description { get; set; }=string.Empty;
    
    public DateTime Date { get; set; }
    
    public string? Priority { get; set; }=string.Empty;

    public bool? Completed { get; set; }
    
    public int? ProjectId { get; set; }
    
    public int? AssignedTo { get; set; }
    
    public List<MessageDto>? Messages { get; set; }
    public List<EmployeeDto>? Followers { get; set; }
}
