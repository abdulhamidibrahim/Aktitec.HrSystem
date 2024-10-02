using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class RevisorsRepo(HrSystemDbContext context) : GenericRepo<Revisor>(context), IRevisorsRepo
{
    public Task<Revisor?> GetRevisorByUserIdAsync(int userId, int documentId)
    {
        if (context.Revisors != null)
            return context.Revisors
                .Include(x => x.Employee)
                .Include(x => x.Document)
                .FirstOrDefaultAsync(x => x.EmployeeId == userId && x.DocumentId == documentId);
        
        return Task.FromResult(new Revisor());
    }

    public bool RevisionCommited(int documentId)
    {
        var result = true;
        if (context.Revisors != null)
        {
            var revisor = context.Revisors
                .Include(x => x.Document)
                .Where(x => x.DocumentId == documentId);

            foreach (var element in revisor)
            {
                if (!element.IsReviewed)
                    return false;
            }

            return result;
        }
        return false;
    }
    
    public void CreateDigitalSignature(int documentId)
    {
        var document = context.Documents!
            .Include(document => document.DocumentFiles)
            .Include(x => x.Revisors)
            .ThenInclude(revisor => revisor.Employee)
            .FirstOrDefault(x => x.Id == documentId);
        if (document != null)
            return;
        if( document is { Revisors: not null } && document.Revisors.Any() && RevisionCommited(documentId))
        {
            foreach(var revisor in document.Revisors)
            {
                if (document?.DocumentFiles != null)
                {
                    var hash = string.Empty;
                    foreach (var file in document.DocumentFiles)
                    {
                        hash += file.FileHash + ",";
                    }

                    document.FilesHash = hash;
                }

                if (revisor.Employee.PrivateKey != null)
                    if (document != null)
                        revisor.DigitalSignature = revisor.Employee.PrivateKey.Concat(document.FilesHash).ToString();
            }
        }
    }
}
