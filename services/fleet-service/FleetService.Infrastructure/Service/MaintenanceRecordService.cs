using FleetService.Application.DTOs;
using FleetService.Application.Interfaces;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using FleetService.Infrastructure.Messaging.Events;
using FleetService.Infrastructure.Messaging.Interfaces;

namespace FleetService.Infrastructure.Services;

public class MaintenanceRecordService : IMaintenanceRecordService
{
    private readonly IMaintenanceRecordRepository _repository;

    private readonly IVehicleRepository _vehicleRepository;

    private readonly IKafkaProducer _producer;

    public MaintenanceRecordService(
        IMaintenanceRecordRepository repository,
        IVehicleRepository vehicleRepository,
        IKafkaProducer producer)
    {
        _repository = repository;
        _vehicleRepository = vehicleRepository;
        _producer = producer;
    }

    public async Task<IEnumerable<MaintenanceRecord>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MaintenanceRecord?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<MaintenanceRecord> CreateAsync(
        CreateMaintenanceRecordDto dto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(dto.VehicleId);

        if (vehicle is null)
        {
            throw new InvalidOperationException("Vehicle not found.");
        }

        var maintenanceRecord = new MaintenanceRecord
        {
            Id = Guid.NewGuid(),
            VehicleId = dto.VehicleId,
            ServiceType = dto.ServiceType,
            Description = dto.Description,
            ScheduledDate = dto.ScheduledDate,
            Cost = dto.Cost,
            Vendor = dto.Vendor,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(maintenanceRecord);

        await _producer.ProduceAsync(
            "maintenance.created",
            new MaintenanceCreatedEvent
            {
                Id = maintenanceRecord.Id,
                VehicleId = maintenanceRecord.VehicleId,
                ServiceType = maintenanceRecord.ServiceType,
                ScheduledDate = maintenanceRecord.ScheduledDate,
                Status = maintenanceRecord.Status,
                CreatedAt = maintenanceRecord.CreatedAt
            });

        return maintenanceRecord;
    }

    public async Task<MaintenanceRecord?> UpdateAsync(
        Guid id,
        UpdateMaintenanceRecordDto dto)
    {
        var maintenanceRecord = await _repository.GetByIdAsync(id);

        if (maintenanceRecord is null)
        {
            return null;
        }

        var vehicle = await _vehicleRepository.GetByIdAsync(dto.VehicleId);

        if (vehicle is null)
        {
            throw new InvalidOperationException("Vehicle not found.");
        }

        var previousStatus = maintenanceRecord.Status;

        maintenanceRecord.VehicleId = dto.VehicleId;
        maintenanceRecord.ServiceType = dto.ServiceType;
        maintenanceRecord.Description = dto.Description;
        maintenanceRecord.ScheduledDate = dto.ScheduledDate;
        maintenanceRecord.CompletedDate = dto.CompletedDate;
        maintenanceRecord.Cost = dto.Cost;
        maintenanceRecord.Vendor = dto.Vendor;
        maintenanceRecord.Status = dto.Status;
        maintenanceRecord.UpdatedAt = DateTime.UtcNow;
        maintenanceRecord.UpdatedBy = "system";

        if (maintenanceRecord.Status == "Completed" &&
            maintenanceRecord.CompletedDate is null)
        {
            maintenanceRecord.CompletedDate = DateTime.UtcNow;
        }

        await _repository.UpdateAsync(maintenanceRecord);

        if (previousStatus != "Completed" &&
            maintenanceRecord.Status == "Completed")
        {
            await _producer.ProduceAsync(
                "maintenance.completed",
                new MaintenanceCompletedEvent
                {
                    Id = maintenanceRecord.Id,
                    VehicleId = maintenanceRecord.VehicleId,
                    ServiceType = maintenanceRecord.ServiceType,
                    CompletedDate = maintenanceRecord.CompletedDate!.Value,
                    Cost = maintenanceRecord.Cost
                });
        }

        return maintenanceRecord;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var maintenanceRecord = await _repository.GetByIdAsync(id);

        if (maintenanceRecord is null)
        {
            return false;
        }

        maintenanceRecord.IsDeleted = true;
        maintenanceRecord.UpdatedAt = DateTime.UtcNow;
        maintenanceRecord.UpdatedBy = "system";

        await _repository.UpdateAsync(maintenanceRecord);

        await _producer.ProduceAsync(
            "maintenance.deleted",
            new MaintenanceDeletedEvent
            {
                Id = maintenanceRecord.Id,
                VehicleId = maintenanceRecord.VehicleId,
                DeletedAt = maintenanceRecord.UpdatedAt.Value
            });

        return true;
    }
}
