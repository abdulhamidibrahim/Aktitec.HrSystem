using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class RevisorManager(
    UserUtility userUtility,
    IUnitOfWork unitOfWork) : IRevisorManager
{
    public Task<int> Add(RevisorAddDto revisorAddDto)
    {
        var revisor = new Revisor()
        {
          EmployeeId = revisorAddDto.EmployeeId,
          DocumentId = revisorAddDto.DocumentId,
          IsReviewed = true,
          // Notes = revisorAddDto.Notes,
          RevisionDate = DateTime.Now,
          
        };
        
        unitOfWork.Revisors.Add(revisor);
        return unitOfWork.SaveChangesAsync();
    }

    public async Task<int> LogNote(RevisorUpdateDto revisorUpdateDto, int employeeId, int documentId)
    {
        // var userId = userUtility.GetUserId();
        // var newUserId = int.TryParse(userId, out var id) ? id : 0;
        var revisor = await unitOfWork.Revisors.GetRevisorByUserIdAsync(employeeId,documentId);

        if (revisor == null) return 0;

        // revisor.IsReviewed = true;
        if (!revisorUpdateDto.Notes.IsNullOrEmpty()) revisor.Notes = revisorUpdateDto.Notes;
        revisor.RevisionDate = DateTime.Now;
        
        
        unitOfWork.Revisors.Update(revisor);
        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> ConfirmRevision(int employeeId,int documentId)
    {
        // var userId = userUtility.GetUserId();
        // var newUserId = int.TryParse(userId, out var id) ? id : 0;
        var revisor = await unitOfWork.Revisors.GetRevisorByUserIdAsync(employeeId,documentId);
        if (revisor == null) return 0;
        revisor.IsReviewed = true;
        unitOfWork.Revisors.CreateDigitalSignature(documentId);
        var document = unitOfWork.Documents.GetById(documentId);
        if (document != null) document.Revision++;
        
        if (unitOfWork.Revisors.RevisionCommited(documentId))
        {
            if (document != null)
            {
                document.Version++;
                document.UniqueName = document.DocumentCode + "_V" + document.Version + "_R" + document.Revision;
            }
        }
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var revisor = unitOfWork.Revisors.GetById(id);
        if (revisor==null) return Task.FromResult(0);
        revisor.IsDeleted = true;
        revisor.DeletedAt = DateTime.Now;
        revisor.DeletedBy = userUtility.GetUserId();
        unitOfWork.Revisors.Update(revisor);
        return unitOfWork.SaveChangesAsync();
    }

    

}
