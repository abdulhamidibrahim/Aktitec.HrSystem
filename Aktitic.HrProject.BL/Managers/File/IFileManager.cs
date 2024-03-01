using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IFileManager
{
    public void Add(FileAddDto fileAddDto);
    public void Update(FileUpdateDto fileUpdateDto);
    public void Delete(FileDeleteDto fileDeleteDto);
    public FileReadDto? Get(int id);
    // public FileReadDto? GetByEmployeeId(int id);
    public List<FileReadDto> GetAll();
}