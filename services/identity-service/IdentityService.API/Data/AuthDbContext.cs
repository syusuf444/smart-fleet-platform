using IdentityService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(
        DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
}