using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aktitic.HrProject.DAL.Models;

public class Message : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string? SenderName { get; set; }=string.Empty;
    public string? SenderPhotoUrl { get; set; }=string.Empty;
        
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public string? GroupName { get; set; }
    
    public DateTime? Date { get; set; }
    public string? Text { get; set; }
    public string? FileName { get; set; }
    
    [NotMapped]
    public Dictionary<string,string>? FilePath { get; set; }
    
    public Task? Task { get; set; }
    public int? TaskId { get; set; }
}