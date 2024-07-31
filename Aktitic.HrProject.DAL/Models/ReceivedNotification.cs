namespace Aktitic.HrProject.DAL.Models;

public class ReceivedNotification : BaseEntity
{
    public int Id { get; set; }
    public int NotificationId { get; set; }
    public bool IsRead { get; set; }
    public int UserId { get; set; }
}