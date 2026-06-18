using FleetService.Application.Features.FuelRecords.Commands;
using FleetService.Application.Services;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Handlers;

public class DeleteFuelRecordHandler
    : IRequestHandler<DeleteFuelRecordCommand, bool>
{
    private readonly IFuelRecordService _service;

    public DeleteFuelRecordHandler(IFuelRecordService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(
        DeleteFuelRecordCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id);
    }
}
