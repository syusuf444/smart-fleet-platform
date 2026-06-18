using FleetService.Application.Features.Vehicles.Commands;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Vehicles.Handlers;

public class CreateVehicleHandler
    : IRequestHandler<CreateVehicleCommand, Vehicle>
{
    private readonly IVehicleService _service;

    public CreateVehicleHandler(IVehicleService service)
    {
        _service = service;
    }

    public async Task<Vehicle> Handle(
        CreateVehicleCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.VehicleDto);
    }
}