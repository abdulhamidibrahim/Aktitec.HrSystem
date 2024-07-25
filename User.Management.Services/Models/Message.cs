using MimeKit;

namespace User.Management.Services.Models;

public class Message
{
    public List<MailboxAddress> To { get; set; } = new List<MailboxAddress>();
    public string Subject { get; set; }
    public string Content { get; set; }

    public Message(IEnumerable<string> to, string subject, string content)
    {
        To.AddRange(to.Select(m=>new MailboxAddress("Email",m)));
        
        Subject = subject;
        
        Content = content;
    }
}