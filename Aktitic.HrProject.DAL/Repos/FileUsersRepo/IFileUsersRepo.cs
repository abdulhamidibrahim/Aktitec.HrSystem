using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IFileUsersRepo :IGenericRepo<FileUsers>
{
    Task<List<FileUsers>> GetAllFileUsers(int fileId);
}