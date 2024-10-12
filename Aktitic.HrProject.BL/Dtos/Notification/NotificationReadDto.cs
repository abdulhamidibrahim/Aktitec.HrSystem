using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class NotificationReadDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }

    public bool IsAll { get; set; }
    public bool IsAdmin { get; set; }
    
    public Priority Priority { get; set; }

    public IEnumerable<ReceivedNotificationDto>? Receivers { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
