using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class AppSubModulesRepo(HrSystemDbContext context) : GenericRepo<AppSubModule>(context), IAppSubModulesRepo
{
   
}
