using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class EmailsDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string ReceiverEmail { get; set; }
    public string? Cc { get; set; }
    public string? Bcc { get; set; }
    public string Subject { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string? Label { get; set; }
    public bool Read { get; set; }
    public bool Archive { get; set; }
    public bool Starred { get; set; }
    public bool Draft { get; set; }
    
    public bool Trash { get; set; }
    
    public bool Selected { get; set; }
    
    public bool Spam { get; set; }
    
    public ICollection<AttachmentDto>? Attachments { get; set; }
    
    
    public ApplicationUserDto Sender { get; set; }
    
    public ApplicationUserDto Receiver { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}