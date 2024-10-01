using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface IDocumentFilesRepo :IGenericRepo<DocumentFile>
{
    Task<List<DocumentFile>> GetDocumentFiles(int documentId);
    public Task<List<DocumentFile>> GetAllDocumentFilesAsync(int documentId, int page = 1, int pageSize = 10);
    public Task<int> AddDocumentFileAsync(List<DocumentFile> documentFile);
    public Task<Task> DeleteDocumentAsync(int id);
}
