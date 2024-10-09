using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public partial class Document : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Version { get; set; }
    
    public int First { get; set; }
    public int Last { get; set; }
    public int? Next { get; set; }
    public int? Previous { get; set; }
    public string? FilesHash { get; set; }
    public string? UniqueName { get; set; }
    public string DocumentCode { get; set; }
    public string Type { get; set; }
    public int Revision { get; set; }
    public string Title { get; set; }
    public string? PrintSize { get; set; }
    public string Status { get; set; }
    public string? Description { get; set; }
    public Confidential Confidential { get; set; }
    // public string? DigitalSignature { get; set; }
    public List<DocumentFile>? DocumentFiles { get; set; }
    
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public Project? Project { get; set; }
    [ForeignKey(nameof(Project))]
    public int? ProjectId { get; set; }

    public List<FileUsers>? FileUsers { get; set; }
    public List<Revisor>? Revisors { get; set; }
}