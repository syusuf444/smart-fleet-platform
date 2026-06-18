using FleetService.Domain.Entities;

namespace FleetService.Application.Interfaces;

public interface IFuelRecordRepository
{
    Task<IEnumerable<FuelRecord>> GetAllAsync();

    Task<FuelRecord?> GetByIdAsync(Guid id);

    Task AddAsync(FuelRecord fuelRecord);

    Task UpdateAsync(FuelRecord fuelRecord);
}
