using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

public class SeedDataService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<int>> roleManager,
    ILogger<SeedDataService> logger,
    IUnitOfWork unitOfWork)
{
    public async Task SeedDataAsync()
    {
        try
        {
            // create database transaction 
            // await unitOfWork.BeginTransactionAsync();            
            // Check if the admin role exists, if not, create it
            if (!await roleManager.RoleExistsAsync("SystemOwner"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("SystemOwner"));
            }

            // Check if the admin user exists, if not, create it
            var adminUser = await userManager.FindByNameAsync("admin");
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

                var result = await userManager.CreateAsync(adminUser, "aktitech_admin@123"); // Set a strong password
                if (result.Succeeded)
                {
                    // Assign the SystemOwner role to the admin user
                    await userManager.AddToRoleAsync(adminUser, "SystemOwner");

                    // Save changes to get the adminUser.Id
                    await unitOfWork.SaveChangesAsync();

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
                        Logo = "",
                        Language = "",
                        Postal = "12345",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        ManagerId = adminUser.Id // Associate the company with the admin user
                    };

                    var companyId  =await unitOfWork.Company.Create(company);

                    adminUser.TenantId = companyId;
                    
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    // Handle errors (log or throw)
                    logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new Exception("Failed to create admin user");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}