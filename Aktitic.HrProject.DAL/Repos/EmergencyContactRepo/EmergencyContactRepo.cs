using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class EmergencyContactRepo(HrSystemDbContext context) : GenericRepo<EmergencyContact>(context), IEmergencyContactRepo
{
    private readonly HrSystemDbContext _context = context;

    public async Task<EmergencyContact?> GetByUserId(int userId)
    {
        if (_context.EmergencyContacts != null)
            return await _context.EmergencyContacts
                .FirstOrDefaultAsync(x=>x.UserId == userId);
       
        return new EmergencyContact();
    }
}