using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

public class SeedDataService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SeedDataService> _logger;

    public SeedDataService(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole<int>> roleManager,
        ILogger<SeedDataService> logger,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedDataAsync()
    {
        try
        {
            // Check if the admin role exists, if not, create it
            if (!await _roleManager.RoleExistsAsync("SystemOwner"))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>("SystemOwner"));
            }

            // Check if the admin user exists, if not, create it
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                // Create the admin user
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@system.com",
                    FirstName = "Aktitech",
                    LastName = "Company",
                    Password = "",
                    EmailConfirmed = true,
                    IsAdmin = true,
                    HasAccess = true,
                    CreatedBy = "admin",
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(adminUser, "aktitech_admin@123"); // Set a strong password
                if (result.Succeeded)
                {
                    // Assign the SystemOwner role to the admin user
                    await _userManager.AddToRoleAsync(adminUser, "SystemOwner");

                    // Save changes to get the adminUser.Id
                    await _unitOfWork.SaveChangesAsync();

                    // Create the company
                    var company = new Company
                    {
                        CompanyName = "Admin Company",
                        Email = "admin@company.com",
                        Phone = "123456789",
                        Address = "123 Admin St",
                        Website = "https://admincompany.com",
                        Fax = "123456789",
                        Country = "Country",
                        City = "City",
                        Contact = "Admin Contact",
                        State = "State",
                        Postal = "12345",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        ManagerId = adminUser.Id // Associate the company with the admin user
                    };

                    var companyId  =await _unitOfWork.Company.Create(company);

                    adminUser.TenantId = companyId;
                    
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    // Handle errors (log or throw)
                    _logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new Exception("Failed to create admin user");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}