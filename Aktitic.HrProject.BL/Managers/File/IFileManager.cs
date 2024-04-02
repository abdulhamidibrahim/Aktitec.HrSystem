using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IFileManager
{
    public Task<int> Add(FileAddDto fileAddDto);
    public Task<int> Update(FileUpdateDto fileUpdateDto, int id);
    public Task<int> Delete(int id);
    public FileReadDto? Get(int id);
    // public FileReadDto? GetByEmployeeId(int id);
    public List<FileReadDto> GetAll();
}