using FleetService.Application.Features.Drivers.Queries;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Handlers;

public class GetDriverByIdHandler
    : IRequestHandler<GetDriverByIdQuery, Driver?>
{
    private readonly IDriverService _service;

    public GetDriverByIdHandler(IDriverService service)
    {
        _service = service;
    }

    public async Task<Driver?> Handle(
        GetDriverByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
