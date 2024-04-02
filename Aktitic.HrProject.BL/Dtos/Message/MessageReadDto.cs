using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.BL;

public class MessageReadDto
{
    public int Id { get; set; }
    
    public string? SenderName { get; set; }=string.Empty;
    public string? SenderPhotoUrl { get; set; }=string.Empty;
        
    public int? SenderId { get; set; }
    
    public DateOnly? Date { get; set; }
    public string? Text { get; set; }
    public FileDto? File { get; set; }
    
    public TaskDto? Task { get; set; }
    public int? TaskId { get; set; }
}
