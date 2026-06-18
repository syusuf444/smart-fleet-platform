using FleetService.Application.Features.Drivers.Commands;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Handlers;

public class UpdateDriverHandler
    : IRequestHandler<UpdateDriverCommand, Driver?>
{
    private readonly IDriverService _service;

    public UpdateDriverHandler(IDriverService service)
    {
        _service = service;
    }

    public async Task<Driver?> Handle(
        UpdateDriverCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request.Id, request.DriverDto);
    }
}
