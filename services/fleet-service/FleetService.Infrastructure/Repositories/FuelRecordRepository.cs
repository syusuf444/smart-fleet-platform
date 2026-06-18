using FleetService.Application.Interfaces;
using FleetService.Domain.Entities;
using FleetService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetService.Infrastructure.Repositories;

public class FuelRecordRepository : IFuelRecordRepository
{
    private readonly FleetDbContext _context;

    public FuelRecordRepository(FleetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FuelRecord>> GetAllAsync()
    {
        return await _context.FuelRecords
            .Where(record => !record.IsDeleted)
            .ToListAsync();
    }

    public async Task<FuelRecord?> GetByIdAsync(Guid id)
    {
        return await _context.FuelRecords
            .FirstOrDefaultAsync(record =>
                record.Id == id &&
                !record.IsDeleted);
    }

    public async Task AddAsync(FuelRecord fuelRecord)
    {
        await _context.FuelRecords.AddAsync(fuelRecord);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FuelRecord fuelRecord)
    {
        _context.FuelRecords.Update(fuelRecord);
        await _context.SaveChangesAsync();
    }
}
