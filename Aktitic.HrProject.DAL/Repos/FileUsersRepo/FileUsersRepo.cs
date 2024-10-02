using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class FileUsersRepo(HrSystemDbContext context) : GenericRepo<FileUsers>(context), IFileUsersRepo
{
    public async Task<List<FileUsers>> GetAllFileUsers(int fileId)
    {
        if (context.FileUsers != null) 
            return await context.FileUsers
                .Where(x => x.DocumentId == fileId)
                .ToListAsync();
        return new List<FileUsers>();
    }
}
