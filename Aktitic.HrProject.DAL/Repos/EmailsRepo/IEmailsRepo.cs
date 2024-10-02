using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IEmailsRepo :IGenericRepo<Email>
{
    IQueryable<Email> GlobalSearch(string? searchKey,string email);
    Email GetEmail(int id);
    Task<IEnumerable<Email>> GetReceivedEmails(string email);
    Task<IEnumerable<Email>> GetSentEmails(int id);
    Task<IEnumerable<Email>> GetStarredEmails(int? page, int? pageSize,string email);
    Task<IEnumerable<Email>> GetArchivedEmails( int? page, int? pageSize,string email);
    Task<IEnumerable<Email>> GetDraftedEmails( int? page, int? pageSize,string email);
    Task<IEnumerable<Email>> GetTrashedEmails( int? page, int? pageSize,string email);

    Task<MailAttachment> GetAttachments(int id);
}