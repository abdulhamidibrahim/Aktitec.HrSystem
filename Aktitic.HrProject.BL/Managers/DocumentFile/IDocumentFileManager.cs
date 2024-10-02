using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public interface IDocumentFileManager
{
    public Task<int> Add(DocumentFileAddDto documentAddDto, int documentId);
    public Task<int> Update(DocumentFileUpdateDto documentUpdateDto, int id);
    public Task<int> Delete(int id);

    DocumentFileReadDto? DownloadFile(int id);
    DocumentFileReadDto? Get(int id);
    public Task<List<DocumentFileReadDto>?> GetDocumentFiles(int documentId,int page = 1, int pageSize = 10);

    
}