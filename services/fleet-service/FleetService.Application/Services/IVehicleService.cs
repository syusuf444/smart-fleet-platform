using FleetService.Application.DTOs;
using FleetService.Domain.Entities;

namespace FleetService.Application.Services;

public interface IVehicleService
{
    Task<IEnumerable<Vehicle>> GetAllAsync();

    Task<Vehicle> CreateAsync(CreateVehicleDto dto);
}