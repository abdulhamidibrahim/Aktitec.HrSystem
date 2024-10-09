using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class TaskUpdateDto
{
    
    public string Text { get; set; }=string.Empty;
    
    public string? Description { get; set; }=string.Empty;
    
    public DateOnly? Date { get; set; }
    
    public string? Priority { get; set; }

    public bool? Completed { get; set; }
    
    public int? ProjectId { get; set; }
    
    public int? AssignedTo { get; set; }
    public List<MessageDto>? Messages { get; set; }
}
