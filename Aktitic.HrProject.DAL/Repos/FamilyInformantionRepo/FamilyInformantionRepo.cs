using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class FamilyInformantionRepo(HrSystemDbContext context) : GenericRepo<FamilyInformation>(context), IFamilyInformantionRepo
{
    private readonly HrSystemDbContext _context = context;

    public async Task<IEnumerable<FamilyInformation>> GetByUserId(int userId)
    {
        if (_context.FamilyInformations != null)
            return await _context.FamilyInformations
                .Where(x => x.UserId == userId)
                .ToListAsync();
       
        return [];
    }
}