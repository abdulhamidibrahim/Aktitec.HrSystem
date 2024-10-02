using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class DocumentFile : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public double Size { get; set; }
    public byte[]? FileHash { get; set; }
    public int FileNumber { get; set; }
    public string Path { get; set; }
    public string? Description { get; set; }
    public Document Document { get; set; }
    [ForeignKey(nameof(Document))]
    public int DocumentId { get; set; }


}