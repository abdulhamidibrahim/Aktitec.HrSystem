using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IFileManager
{
    public Task<int> Add(FileAddDto fileAddDto);
    public Task<int> Update(FileUpdateDto fileUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<List<FileReadDto>>? GetByProjectId(int id);
    // public FileReadDto? GetByEmployeeId(int id);
    public Task<List<FileReadDto>> GetAll();
    
    public Task<FilteredFilesDto> GetFilteredFilesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);
    public Task<List<FileReadDto>> GlobalSearch(string searchKey,string? column);

}