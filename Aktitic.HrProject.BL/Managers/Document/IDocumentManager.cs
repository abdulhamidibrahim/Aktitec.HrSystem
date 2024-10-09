using Aktitic.HrProject.Api.Configuration;

namespace Aktitic.HrProject.BL;

public interface IDocumentManager
{
    public Task<ApiRespone<string>> Add(DocumentAddDto documentAddDto);
    public Task<int> Update(DocumentUpdateDto documentUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<List<DocumentReadDto>?> GetByProjectId(int id);
    public Task<DocumentReadDto?> GetById(int id);
    // public DocumentReadDto? GetByEmployeeId(int id);
    public Task<List<DocumentReadDto>> GetAll();
    public List<DocumentReadDto>? GetFiles(string? type, string? status, int page, int pageSize);
    
    public Task<FilteredDocumentsDto> GetFilteredFilesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);
    public Task<List<DocumentReadDto>> GlobalSearch(string searchKey,string? column);

    
}