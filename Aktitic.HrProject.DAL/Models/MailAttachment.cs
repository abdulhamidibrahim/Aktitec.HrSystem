namespace Aktitic.HrProject.DAL.Models;

public class MailAttachment : BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Type { get; set; }
    public string? Size { get; set; }
    public int EmailId { get; set; }
    public Email Email { get; set; }
}