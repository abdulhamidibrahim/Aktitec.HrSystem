using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class MessageAddDto
{
        
    public int? SenderId { get; set; }
    
    public DateTime? Date { get; set; }
    public string? Text { get; set; }
    // public Documents? Documents { get; set; }
    
    // public Task? Task { get; set; }
    public int? TaskId { get; set; }
}
