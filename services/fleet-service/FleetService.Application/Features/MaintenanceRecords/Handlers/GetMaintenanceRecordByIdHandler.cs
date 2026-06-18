using FleetService.Application.Features.MaintenanceRecords.Queries;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Handlers;

public class GetMaintenanceRecordByIdHandler
    : IRequestHandler<GetMaintenanceRecordByIdQuery, MaintenanceRecord?>
{
    private readonly IMaintenanceRecordService _service;

    public GetMaintenanceRecordByIdHandler(IMaintenanceRecordService service)
    {
        _service = service;
    }

    public async Task<MaintenanceRecord?> Handle(
        GetMaintenanceRecordByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
