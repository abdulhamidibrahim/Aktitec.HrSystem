using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class TaskListDto
{
    public int Id { get; set; }
    public string ListName { get; set; } = string.Empty;
    public string  Priority { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool? Status { get; set; }
    public int? TaskId { get; set; }
    public int TaskBoardId { get; set; }
    public TaskDto Tasks { get; set; }=new();
    
}