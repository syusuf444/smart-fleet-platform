using FleetService.Application.Features.Drivers.Commands;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Handlers;

public class CreateDriverHandler
    : IRequestHandler<CreateDriverCommand, Driver>
{
    private readonly IDriverService _service;

    public CreateDriverHandler(IDriverService service)
    {
        _service = service;
    }

    public async Task<Driver> Handle(
        CreateDriverCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.DriverDto);
    }
}
