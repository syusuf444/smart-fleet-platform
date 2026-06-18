using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Queries;

public record GetAllMaintenanceRecordsQuery : IRequest<IEnumerable<MaintenanceRecord>>;
