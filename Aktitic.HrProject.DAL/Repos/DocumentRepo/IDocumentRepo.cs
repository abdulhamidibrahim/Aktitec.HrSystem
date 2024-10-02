using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IDocumentRepo :IGenericRepo<Document>
{
    Task<IEnumerable<Document>> GlobalSearch(string search);
    Task<IEnumerable<Document>> GetByProjectId(int projectId);
    IEnumerable<Document> GetDocuments(string? status, string? type, int page,int pageSize);
    Task<Document> GetFileById(int id);
    Task<IEnumerable<Document>> GetDocumentWithRevision(); 
    void ComputeHash(int documentId); 
    bool IsUniqueCode(string documentCode);
    Task<IEnumerable<Document>> GetDocumentChain(string documentCode);

}