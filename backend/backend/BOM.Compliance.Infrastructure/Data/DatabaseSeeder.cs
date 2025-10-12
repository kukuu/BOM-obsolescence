using Microsoft.EntityFrameworkCore;
using BOM.Compliance.Domain.Entities;

namespace BOM.Compliance.Infrastructure.Data;

public class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            var users = new[]
            {
                new User 
                { 
                    Id = Guid.NewGuid(), 
                    Email = "analyst@company.com", 
                    Name = "Data Analyst",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("analyst123"),
                    Role = UserRole.DataAnalyst
                },
                new User 
                { 
                    Id = Guid.NewGuid(), 
                    Email = "manager@company.com", 
                    Name = "Senior Manager",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"),
                    Role = UserRole.SeniorManager
                }
            };
            
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
        
        if (!await context.Materials.AnyAsync())
        {
            var materials = new[]
            {
                new Material
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = "RES-001",
                    Name = "1K Ohm Resistor",
                    Type = MaterialType.ElectronicComponent,
                    Manufacturer = "Texas Instruments",
                    ProductionYear = 2023,
                    Status = MaterialStatus.Active,
                    Compliance = new ComplianceData
                    {
                        RoHSStatus = "Compliant",
                        REACHStatus = "Compliant"
                    }
                },
                new Material
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = "CAP-001", 
                    Name = "100uF Capacitor",
                    Type = MaterialType.ElectronicComponent,
                    Manufacturer = "Murata",
                    ProductionYear = 2022,
                    Status = MaterialStatus.NotRecommendedForNewDesigns,
                    Compliance = new ComplianceData
                    {
                        RoHSStatus = "Compliant",
                        REACHStatus = "Compliant"
                    }
                }
            };
            
            await context.Materials.AddRangeAsync(materials);
            await context.SaveChangesAsync();
        }
    }
}