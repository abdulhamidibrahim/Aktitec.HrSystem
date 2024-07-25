using Aktitic.HrProject.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aktitic.HrProject.DAL.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMigrate(this IServiceCollection services)
    {
        // Create a service scope factory once
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        
        // Resolve the database context
        var context = scope.ServiceProvider.GetRequiredService<HrSystemDbContext>();
        
        // Apply migrations
        context.Database.Migrate();
        
        // Return the service collection
        return services;
    }
}