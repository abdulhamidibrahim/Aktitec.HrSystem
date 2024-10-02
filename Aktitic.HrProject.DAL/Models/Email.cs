

namespace Aktitic.HrProject.DAL.Models;

public class Email : BaseEntity
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
    
    public ICollection<MailAttachment> Attachments { get; set; }
    
    
    public ApplicationUser Sender { get; set; }
    
    public ApplicationUser Receiver { get; set; }
    
}
