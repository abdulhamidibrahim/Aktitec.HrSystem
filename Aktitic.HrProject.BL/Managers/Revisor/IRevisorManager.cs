using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IRevisorManager
{
    public Task<int> Add(RevisorAddDto revisorAddDto);
    public Task<int> LogNote(RevisorUpdateDto revisorUpdateDto,int employeeId, int documentId);
    public Task<int> ConfirmRevision(int employeeId,int documentId);
    public Task<int> Delete(int id);
   
  
}