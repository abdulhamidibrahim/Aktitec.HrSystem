using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class DocumentAddDto
{
    public string DocumentCode { get; set; } = string.Empty;
    
    public Confidential Confidential { get; set; }
    public string? Revisors { get; set; } 
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    // public string Revision { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? PrintSize { get; set; } = string.Empty;
    
    public string? Description { get; set; } = string.Empty;
    public List<IFormFile>? Files { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string? FileUsers { get; set; }
    public int?  ProjectId { get; set; } 
    

}