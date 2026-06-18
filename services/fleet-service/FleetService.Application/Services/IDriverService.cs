using FleetService.Application.DTOs;
using FleetService.Domain.Entities;

namespace FleetService.Application.Services;

public interface IDriverService
{
    Task<IEnumerable<Driver>> GetAllAsync();

    Task<Driver?> GetByIdAsync(Guid id);

    Task<Driver> CreateAsync(CreateDriverDto dto);

    Task<Driver?> UpdateAsync(Guid id, UpdateDriverDto dto);

    Task<bool> DeleteAsync(Guid id);
}
