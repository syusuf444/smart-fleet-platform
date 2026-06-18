using FleetService.Application.Interfaces;
using FleetService.Domain.Entities;
using FleetService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetService.Infrastructure.Repositories;

public class MaintenanceRecordRepository : IMaintenanceRecordRepository
{
    private readonly FleetDbContext _context;

    public MaintenanceRecordRepository(FleetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceRecord>> GetAllAsync()
    {
        return await _context.MaintenanceRecords
            .Where(record => !record.IsDeleted)
            .ToListAsync();
    }

    public async Task<MaintenanceRecord?> GetByIdAsync(Guid id)
    {
        return await _context.MaintenanceRecords
            .FirstOrDefaultAsync(record =>
                record.Id == id &&
                !record.IsDeleted);
    }

    public async Task AddAsync(MaintenanceRecord maintenanceRecord)
    {
        await _context.MaintenanceRecords.AddAsync(maintenanceRecord);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MaintenanceRecord maintenanceRecord)
    {
        _context.MaintenanceRecords.Update(maintenanceRecord);
        await _context.SaveChangesAsync();
    }
}
