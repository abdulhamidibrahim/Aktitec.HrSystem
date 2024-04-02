
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class FileManager:IFileManager
{
    private readonly IFileRepo _fileRepo;
    private readonly IUnitOfWork _unitOfWork;

    public FileManager(IFileRepo fileRepo, IUnitOfWork unitOfWork)
    {
        _fileRepo = fileRepo;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(FileAddDto fileAddDto)
    {
        var file = new File()
        {
            Name = fileAddDto.Name,
            Content = fileAddDto.Content,
            Extension = fileAddDto.Extension,
        };
        _fileRepo.Add(file);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(FileUpdateDto fileUpdateDto, int id)
    {
        var file = _fileRepo.GetById(id);
        
        if (file == null) return Task.FromResult(0);
        if(fileUpdateDto.Name != null) file.Name = fileUpdateDto.Name;
        if(fileUpdateDto.Content != null) file.Content = fileUpdateDto.Content;
        if(fileUpdateDto.Extension != null) file.Extension = fileUpdateDto.Extension;
        
        
        _fileRepo.Update(file);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var file = _fileRepo.GetById(id);
        if (file != null) _fileRepo.Delete(file);
        
        return _unitOfWork.SaveChangesAsync();
    }

    public FileReadDto? Get(int id)
    {
        var file = _fileRepo.GetById(id);
        if (file == null) return null;
        return new FileReadDto()
        {
            Id = file.Id,
            Name = file.Name,
            Content = file.Content,
            Extension = file.Extension,
            
        };
    }

    // public FileReadDto? GetByEmployeeId(int id)
    // {
    //     var file = _fileRepo.GetByEmployeeId(id);
    //     if (file.Result == null) return null;
    //     return new FileReadDto()
    //     {
    //         Name = file.Result.Name,
    //         Content = file.Result.Content,
    //         Extension = file.Result.Extension,
    //     };
    // }

    public List<FileReadDto> GetAll()
    {
        var files = _fileRepo.GetAll();
        return files.Result.Select(file => new FileReadDto()
        {
            Id = file.Id,
            Name = file.Name,
            Content = file.Content,
            Extension = file.Extension,
           
        }).ToList();
    }
}
