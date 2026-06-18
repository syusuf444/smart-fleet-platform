using FleetService.Application.Features.MaintenanceRecords.Commands;
using FleetService.Application.Services;
using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Handlers;

public class DeleteMaintenanceRecordHandler
    : IRequestHandler<DeleteMaintenanceRecordCommand, bool>
{
    private readonly IMaintenanceRecordService _service;

    public DeleteMaintenanceRecordHandler(IMaintenanceRecordService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(
        DeleteMaintenanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id);
    }
}
