using FleetService.Application.DTOs;
using FleetService.Domain.Entities;

namespace FleetService.Application.Services;

public interface IFuelRecordService
{
    Task<IEnumerable<FuelRecord>> GetAllAsync();

    Task<FuelRecord?> GetByIdAsync(Guid id);

    Task<FuelRecord> CreateAsync(CreateFuelRecordDto dto);

    Task<FuelRecord?> UpdateAsync(Guid id, UpdateFuelRecordDto dto);

    Task<bool> DeleteAsync(Guid id);
}
