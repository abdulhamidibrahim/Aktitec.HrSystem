namespace Aktitic.HrProject.DAL.Models;

public class FileUsers : BaseEntity
{
    public int Id { get; set; }
    public int FileId { get; set; }
    public int FileUserId { get; set; }
    public Status Status { get; set; }
    public File File { get; set; }
    public ApplicationUser FileUser { get; set; }
}