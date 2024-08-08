namespace Aktitic.HrProject.DAL.Models;

public class ChatGroup : BaseEntity
{
 
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; } = string.Empty;
    public ICollection<ChatGroupUser>? ChatGroupUsers { get; set; }
    public ICollection<Message>? Messages { get; set; }
}