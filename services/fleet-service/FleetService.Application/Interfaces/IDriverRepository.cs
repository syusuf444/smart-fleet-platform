using FleetService.Domain.Entities;

namespace FleetService.Application.Interfaces;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllAsync();

    Task<Driver?> GetByIdAsync(Guid id);

    Task<Driver?> GetByLicenseNumberAsync(string licenseNumber);

    Task AddAsync(Driver driver);

    Task UpdateAsync(Driver driver);
}
