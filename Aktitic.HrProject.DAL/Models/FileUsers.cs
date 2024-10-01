using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class FileUsers : BaseEntity
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(Document))]
    public int DocumentId { get; set; }
    
    [ForeignKey(nameof(FileUser))]
    public int FileUserId { get; set; }

    public bool? Read { get; set; }
    
    public bool? Write { get; set; }
    public Document Document { get; set; }
    public ApplicationUser FileUser { get; set; }
}