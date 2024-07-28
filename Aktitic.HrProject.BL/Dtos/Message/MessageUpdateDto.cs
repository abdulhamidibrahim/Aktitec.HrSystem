using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class MessageUpdateDto
{
    
    public string? SenderName { get; set; }=string.Empty;
    public string? SenderPhotoUrl { get; set; }=string.Empty;
        
    public int? SenderId { get; set; }
    
    public DateTime? Date { get; set; }
    public string? Text { get; set; }
    
    public int? TaskId { get; set; }
}
