namespace Aktitic.HrProject.DAL.Models;

public class ChatGroupUser :BaseEntity
{
    public int ChatGroupId { get; set; }
    public ChatGroup ChatGroup { get; set; }
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsAdmin { get; set; }
}