using FleetService.Application.Features.Vehicles.Queries;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Vehicles.Handlers;

public class GetAllVehiclesHandler
    : IRequestHandler<GetAllVehiclesQuery, IEnumerable<Vehicle>>
{
    private readonly IVehicleService _service;

    public GetAllVehiclesHandler(IVehicleService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<Vehicle>> Handle(
        GetAllVehiclesQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync();
    }
}