using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class TaskDto
{
    public int Id { get; set; }
    
    public string Text { get; set; }=string.Empty;
    
    public string? Description { get; set; }=string.Empty;
    
    public DateTime Date { get; set; }
    
    public string? Priority { get; set; }=string.Empty;

    public bool? Completed { get; set; }
    
    public int? ProjectId { get; set; }
    
    public ProjectDto? Project { get; set; }=null!;
    
    
    public int? AssignedTo { get; set; }
    public List<MessageDto>? Message { get; set; }=null!;

    public EmployeeDto? AssignEmployee { get; set; }=null!;
    //messages
    
}