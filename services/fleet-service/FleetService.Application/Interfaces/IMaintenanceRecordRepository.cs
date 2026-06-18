using FleetService.Domain.Entities;

namespace FleetService.Application.Interfaces;

public interface IMaintenanceRecordRepository
{
    Task<IEnumerable<MaintenanceRecord>> GetAllAsync();

    Task<MaintenanceRecord?> GetByIdAsync(Guid id);

    Task AddAsync(MaintenanceRecord maintenanceRecord);

    Task UpdateAsync(MaintenanceRecord maintenanceRecord);
}
