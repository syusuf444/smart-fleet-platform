using FleetService.Application.Features.MaintenanceRecords.Commands;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Handlers;

public class CreateMaintenanceRecordHandler
    : IRequestHandler<CreateMaintenanceRecordCommand, MaintenanceRecord>
{
    private readonly IMaintenanceRecordService _service;

    public CreateMaintenanceRecordHandler(IMaintenanceRecordService service)
    {
        _service = service;
    }

    public async Task<MaintenanceRecord> Handle(
        CreateMaintenanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.MaintenanceRecordDto);
    }
}
