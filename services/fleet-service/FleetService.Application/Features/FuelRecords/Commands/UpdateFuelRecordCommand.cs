using FleetService.Application.DTOs;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Commands;

public record UpdateFuelRecordCommand(
    Guid Id,
    UpdateFuelRecordDto FuelRecordDto
) : IRequest<FuelRecord?>;
