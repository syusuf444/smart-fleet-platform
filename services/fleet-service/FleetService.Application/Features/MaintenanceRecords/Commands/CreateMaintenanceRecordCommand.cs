using FleetService.Application.DTOs;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.MaintenanceRecords.Commands;

public record CreateMaintenanceRecordCommand(
    CreateMaintenanceRecordDto MaintenanceRecordDto
) : IRequest<MaintenanceRecord>;
