using FleetService.Application.Features.MaintenanceRecords.Queries;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Handlers;

public class GetAllMaintenanceRecordsHandler
    : IRequestHandler<GetAllMaintenanceRecordsQuery, IEnumerable<MaintenanceRecord>>
{
    private readonly IMaintenanceRecordService _service;

    public GetAllMaintenanceRecordsHandler(IMaintenanceRecordService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<MaintenanceRecord>> Handle(
        GetAllMaintenanceRecordsQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync();
    }
}
