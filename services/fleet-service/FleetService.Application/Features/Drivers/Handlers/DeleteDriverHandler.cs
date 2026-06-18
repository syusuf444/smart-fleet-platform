using FleetService.Application.Features.Drivers.Commands;
using FleetService.Application.Services;
using MediatR;

namespace FleetService.Application.Features.Drivers.Handlers;

public class DeleteDriverHandler
    : IRequestHandler<DeleteDriverCommand, bool>
{
    private readonly IDriverService _service;

    public DeleteDriverHandler(IDriverService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(
        DeleteDriverCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id);
    }
}
