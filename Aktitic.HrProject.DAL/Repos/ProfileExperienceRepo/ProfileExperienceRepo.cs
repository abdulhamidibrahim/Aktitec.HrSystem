using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class ProfileExperienceRepo(HrSystemDbContext context) : GenericRepo<ProfileExperience>(context), IProfileExperienceRepo
{
    private readonly HrSystemDbContext _context = context;

    public async Task<IEnumerable<ProfileExperience>> GetByUserId(int userId)
    {
        if (_context.ProfileExperiences != null)
            return await _context.ProfileExperiences
                .Where(x => x.UserId == userId)
                .ToListAsync();
       
        return [];
    }
}