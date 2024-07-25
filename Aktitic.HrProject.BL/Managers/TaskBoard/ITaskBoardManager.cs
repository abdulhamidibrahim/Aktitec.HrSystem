using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public interface ITaskBoardManager
{
    public Task<int> Add(TaskBoardAddDto taskBoardAddDto);
    public Task<int> Update(TaskBoardUpdateDto taskBoardUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<TaskBoardReadDto>? Get(int id);
    public Task<List<TaskBoardReadDto>> GetAll();
    
    public Task<List<TaskBoardReadDto>> GetAllByProjectId(int projectId);
  
}