using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class EducationInformationRepo(HrSystemDbContext context) : GenericRepo<EducationInformation>(context), IEducationInformationRepo
{
    private readonly HrSystemDbContext _context = context;

    public async Task<IEnumerable<EducationInformation>> GetByUserId(int userId)
    {
        if (_context.EducationInformations != null)
            return await _context.EducationInformations
                .Where(x => x.UserId == userId)
                .ToListAsync();
       
        return [];
    }
}