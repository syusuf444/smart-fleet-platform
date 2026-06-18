using FleetService.Application.Features.Drivers.Queries;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Handlers;

public class GetAllDriversHandler
    : IRequestHandler<GetAllDriversQuery, IEnumerable<Driver>>
{
    private readonly IDriverService _service;

    public GetAllDriversHandler(IDriverService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<Driver>> Handle(
        GetAllDriversQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync();
    }
}
