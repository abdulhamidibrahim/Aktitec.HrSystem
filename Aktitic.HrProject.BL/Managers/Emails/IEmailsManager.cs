using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IEmailsManager
{
    public Task<int> Add(EmailsAddDto emailsAddDto);
    public Task<int> Update(EmailsUpdateDto emailsUpdateDto, int id);
    public Task<int> Delete(int id);
    public EmailsReadDto? Get(int id);
    public Task<List<EmailsReadDto>> GetAll(string email);
    public Task<List<EmailsReadDto>> GetSentEmails(int id);
    
    public Task<FilteredEmailsDto> GetFilteredEmailsAsync(string? column, string? value1, string? operator1, string? value2, 
        string? operator2, int page, int pageSize, string email);

    public Task<List<EmailsDto>> GlobalSearch(string searchKey,string? column,string email);
    
    public Task<IEnumerable<EmailsDto>> GetStarredEmails(int? page, int? pageSize, string email);
    public Task<IEnumerable<EmailsDto>> GetTrashedEmails(int? page, int? pageSize, string email);
    public Task<IEnumerable<EmailsDto>> GetArchivedEmails(int? page, int? pageSize, string email);
   public Task<IEnumerable<EmailsDto>> GetDraftEmails(int? page, int? pageSize, string email);
   Task<AttachmentDto> GetAttachments(int id);
}