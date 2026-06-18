using FleetService.Application.DTOs;
using FleetService.Application.Interfaces;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using FleetService.Infrastructure.Messaging.Events;
using FleetService.Infrastructure.Messaging.Interfaces;

namespace FleetService.Infrastructure.Services;

public class FuelRecordService : IFuelRecordService
{
    private readonly IFuelRecordRepository _repository;

    private readonly IVehicleRepository _vehicleRepository;

    private readonly IKafkaProducer _producer;

    public FuelRecordService(
        IFuelRecordRepository repository,
        IVehicleRepository vehicleRepository,
        IKafkaProducer producer)
    {
        _repository = repository;
        _vehicleRepository = vehicleRepository;
        _producer = producer;
    }

    public async Task<IEnumerable<FuelRecord>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<FuelRecord?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<FuelRecord> CreateAsync(CreateFuelRecordDto dto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(dto.VehicleId);

        if (vehicle is null)
        {
            throw new InvalidOperationException("Vehicle not found.");
        }

        var fuelRecord = new FuelRecord
        {
            Id = Guid.NewGuid(),
            VehicleId = dto.VehicleId,
            FuelDate = dto.FuelDate,
            Quantity = dto.Quantity,
            Cost = dto.Cost,
            OdometerReading = dto.OdometerReading,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(fuelRecord);

        await _producer.ProduceAsync(
            "fuel.added",
            new FuelAddedEvent
            {
                Id = fuelRecord.Id,
                VehicleId = fuelRecord.VehicleId,
                FuelDate = fuelRecord.FuelDate,
                Quantity = fuelRecord.Quantity,
                Cost = fuelRecord.Cost,
                OdometerReading = fuelRecord.OdometerReading,
                CreatedAt = fuelRecord.CreatedAt
            });

        return fuelRecord;
    }

    public async Task<FuelRecord?> UpdateAsync(
        Guid id,
        UpdateFuelRecordDto dto)
    {
        var fuelRecord = await _repository.GetByIdAsync(id);

        if (fuelRecord is null)
        {
            return null;
        }

        var vehicle = await _vehicleRepository.GetByIdAsync(dto.VehicleId);

        if (vehicle is null)
        {
            throw new InvalidOperationException("Vehicle not found.");
        }

        fuelRecord.VehicleId = dto.VehicleId;
        fuelRecord.FuelDate = dto.FuelDate;
        fuelRecord.Quantity = dto.Quantity;
        fuelRecord.Cost = dto.Cost;
        fuelRecord.OdometerReading = dto.OdometerReading;
        fuelRecord.UpdatedAt = DateTime.UtcNow;
        fuelRecord.UpdatedBy = "system";

        await _repository.UpdateAsync(fuelRecord);

        await _producer.ProduceAsync(
            "fuel.updated",
            new FuelUpdatedEvent
            {
                Id = fuelRecord.Id,
                VehicleId = fuelRecord.VehicleId,
                Quantity = fuelRecord.Quantity,
                Cost = fuelRecord.Cost,
                UpdatedAt = fuelRecord.UpdatedAt.Value
            });

        return fuelRecord;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var fuelRecord = await _repository.GetByIdAsync(id);

        if (fuelRecord is null)
        {
            return false;
        }

        fuelRecord.IsDeleted = true;
        fuelRecord.UpdatedAt = DateTime.UtcNow;
        fuelRecord.UpdatedBy = "system";

        await _repository.UpdateAsync(fuelRecord);

        await _producer.ProduceAsync(
            "fuel.deleted",
            new FuelDeletedEvent
            {
                Id = fuelRecord.Id,
                VehicleId = fuelRecord.VehicleId,
                DeletedAt = fuelRecord.UpdatedAt.Value
            });

        return true;
    }
}
