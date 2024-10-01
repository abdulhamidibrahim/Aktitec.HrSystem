using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;


namespace Aktitic.HrProject.BL;

public class EmailsAddDto
{
    public required int SenderId { get; set; }
    public required string ReceiverEmail { get; set; }
    public string? Cc { get; set; }
    public string? Bcc { get; set; }
    public required string Subject { get; set; }
    public string? Description { get; set; }
    public required DateTime Date { get; set; }
    public string? Label { get; set; }
    public bool Read { get; set; }
    public bool Archive { get; set; }
    public bool Starred { get; set; }
    
    public bool Draft { get; set; }
    
    public bool Trash { get; set; }
    
    public bool Selected { get; set; }
    
    public bool Spam { get; set; }
    public List<IFormFile>? Attatchments { get; set; }
}
