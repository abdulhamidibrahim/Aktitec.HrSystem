using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class DocumentReadDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public Confidential Confidential { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? UniqueName { get; set; } = string.Empty;
    public string DocumentCode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Revision { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string PrintSize { get; set; } = string.Empty;
    public string Version { get; set; } 
    public int? ProjectId { get; set; }
    public int First { get; set; }
    public int? Previous { get; set; }
    public int? Next { get; set; }
    public int Last { get; set; }
    public List<RevisorDto>? Revisors { get; set; }
    public List<DocumentFileReadDto>? DocumentFiles { get; set; }
    public List<FileUsers>? FileUsers { get; set; }
    // public DateTime RevisionDate { get; set; }
    public DateTime DateCreated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
