using FleetService.Application.Features.MaintenanceRecords.Commands;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Handlers;

public class UpdateMaintenanceRecordHandler
    : IRequestHandler<UpdateMaintenanceRecordCommand, MaintenanceRecord?>
{
    private readonly IMaintenanceRecordService _service;

    public UpdateMaintenanceRecordHandler(IMaintenanceRecordService service)
    {
        _service = service;
    }

    public async Task<MaintenanceRecord?> Handle(
        UpdateMaintenanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(
            request.Id,
            request.MaintenanceRecordDto);
    }
}
