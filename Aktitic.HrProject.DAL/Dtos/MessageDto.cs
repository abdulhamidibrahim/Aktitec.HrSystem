using Aktitic.HrProject.DAL.Pagination.Employee;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class MessageDto
{
    public int Id { get; set; }
    public string? SenderName { get; set; }=string.Empty;
    public string? SenderPhotoUrl { get; set; }=string.Empty;
        
    public int? SenderId { get; set; }
    
    public DateOnly? Date { get; set; }
    public string? Text { get; set; }
    public FileDto? File { get; set; }
}