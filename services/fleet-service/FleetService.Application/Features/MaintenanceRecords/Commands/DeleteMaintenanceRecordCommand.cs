using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Commands;

public record DeleteMaintenanceRecordCommand(Guid Id) : IRequest<bool>;
