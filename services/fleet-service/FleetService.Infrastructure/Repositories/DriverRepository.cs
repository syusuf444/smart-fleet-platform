using FleetService.Application.Interfaces;
using FleetService.Domain.Entities;
using FleetService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetService.Infrastructure.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly FleetDbContext _context;

    public DriverRepository(FleetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Driver>> GetAllAsync()
    {
        return await _context.Drivers
            .Where(driver => !driver.IsDeleted)
            .ToListAsync();
    }

    public async Task<Driver?> GetByIdAsync(Guid id)
    {
        return await _context.Drivers
            .FirstOrDefaultAsync(driver =>
                driver.Id == id &&
                !driver.IsDeleted);
    }

    public async Task<Driver?> GetByLicenseNumberAsync(string licenseNumber)
    {
        return await _context.Drivers
            .FirstOrDefaultAsync(driver =>
                driver.LicenseNumber == licenseNumber &&
                !driver.IsDeleted);
    }

    public async Task AddAsync(Driver driver)
    {
        await _context.Drivers.AddAsync(driver);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Driver driver)
    {
        _context.Drivers.Update(driver);
        await _context.SaveChangesAsync();
    }
}
