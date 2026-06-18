using FleetService.Application.DTOs;
using FleetService.Domain.Entities;

namespace FleetService.Application.Services;

public interface IMaintenanceRecordService
{
    Task<IEnumerable<MaintenanceRecord>> GetAllAsync();

    Task<MaintenanceRecord?> GetByIdAsync(Guid id);

    Task<MaintenanceRecord> CreateAsync(CreateMaintenanceRecordDto dto);

    Task<MaintenanceRecord?> UpdateAsync(Guid id, UpdateMaintenanceRecordDto dto);

    Task<bool> DeleteAsync(Guid id);
}
