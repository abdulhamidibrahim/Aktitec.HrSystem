using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Helpers;

public static class SeedData
{
    public static async Task SeedDataAsync
        (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
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
                // Set other properties as needed
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Set a strong password
            if (result.Succeeded)
            {
                // Assign the SystemOwner role to the admin user
                await userManager.AddToRoleAsync(adminUser, "SystemOwner");
            }
            else
            {
                // Handle errors (log or throw)
                throw new Exception("Failed to create admin user");
            }
        }
    }
}
