using FleetService.Application.DTOs;
using FleetService.Application.Interfaces;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using FleetService.Infrastructure.Messaging.Events;
using FleetService.Infrastructure.Messaging.Interfaces;

namespace FleetService.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;

    private readonly IKafkaProducer _producer;

    public VehicleService(
        IVehicleRepository repository,
        IKafkaProducer producer)
    {
        _repository = repository;
        _producer = producer;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Vehicle> CreateAsync(
        CreateVehicleDto dto)
    {
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            VehicleNumber = dto.VehicleNumber,
            Manufacturer = dto.Manufacturer,
            Model = dto.Model,
            Year = dto.Year,
            FuelCapacity = dto.FuelCapacity,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(vehicle);

        var vehicleCreatedEvent =
            new VehicleCreatedEvent
            {
                Id = vehicle.Id,
                VehicleNumber = vehicle.VehicleNumber,
                Manufacturer = vehicle.Manufacturer,
                Model = vehicle.Model,
                Year = vehicle.Year,
                CreatedAt = vehicle.CreatedAt
            };

        await _producer.ProduceAsync(
            "vehicle-created",
            vehicleCreatedEvent);

        return vehicle;
    }
}