using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using User.Management.Services.Models;

namespace User.Management.Services.Services;

public class EmailService(EmailConfiguration configuration) : IEmailService
{
    public void SendEmail(Message message)
    {
        var messageToSend = CreateMailMessage(message);
        SendEmail(messageToSend);
    }

    private void SendEmail(object messageToSend)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(configuration.SmtpServer, configuration.SmtpPort, SecureSocketOptions.StartTls);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(configuration.Username, configuration.Password);
            client.Send((MimeMessage)messageToSend);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
    
    private object CreateMailMessage(Message message)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add( new MailboxAddress("Email",configuration.From));
        mailMessage.To.AddRange(message.To);
        mailMessage.Subject = message.Subject;
        mailMessage.Body = new TextPart(TextFormat.Text) { Text = message.Content };
        
        return mailMessage;
    }
}