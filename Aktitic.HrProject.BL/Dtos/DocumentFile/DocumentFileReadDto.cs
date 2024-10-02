using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class DocumentFileReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public double Size { get; set; }
   
    public int FileNumber { get; set; }
    public string Path { get; set; }
    public string? Description { get; set; }
   
    public required int DocumentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
