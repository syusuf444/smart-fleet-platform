using IdentityService.API.Data;
using IdentityService.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.API.Extensions;

public static class AuthDbSeeder
{
    public static void Seed(AuthDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        var admin = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FullName = "System Admin",
            Email = "admin@smartfleet.com",
            PasswordHash = HashPassword("Admin@123"),
            Role = "SuperAdmin"
        };

        context.Users.Add(admin);
        context.SaveChanges();
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
