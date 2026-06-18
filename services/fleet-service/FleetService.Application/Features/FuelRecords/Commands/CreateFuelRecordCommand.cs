using FleetService.Application.DTOs;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Commands;

public record CreateFuelRecordCommand(
    CreateFuelRecordDto FuelRecordDto
) : IRequest<FuelRecord>;
