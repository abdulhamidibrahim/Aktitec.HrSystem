using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IEmailsRepo :IGenericRepo<Email>
{
    IQueryable<Email> GlobalSearch(string? searchKey,string email);
    // IQueryable<Email> EmailSearch(int? sender, int? receiver, string column);
    Email GetEmail(int id);
    Task<IEnumerable<Email>> GetReceivedEmails(string email);
    Task<IEnumerable<Email>> GetSentEmails(int id);
    Task<IEnumerable<Email>> GetStarredEmails(int? page, int? pageSize,string email);
    Task<IEnumerable<Email>> GetArchivedEmails( int? page, int? pageSize,string email);
    Task<IEnumerable<Email>> GetDraftedEmails( int? page, int? pageSize,string email);
    Task<IEnumerable<Email>> GetTrashedEmails( int? page, int? pageSize,string email);

    Task<MailAttachment> GetAttachments(int id);
    Task DeleteRange(List<int> ids);
    Task TrashRange(List<int> ids, bool trash);
    public IQueryable<Email> SearchTrashedEmails(int receiver, string searchKey);
    public IQueryable<Email> SearchArchivedEmails(int receiver, string searchKey);
    public IQueryable<Email> SearchStarredEmails(int receiver, string searchKey);
    public IQueryable<Email> SearchDraftedEmails(int receiver, string searchKey);
    public IQueryable<Email> SearchSentEmails(int sender, string searchKey);

}