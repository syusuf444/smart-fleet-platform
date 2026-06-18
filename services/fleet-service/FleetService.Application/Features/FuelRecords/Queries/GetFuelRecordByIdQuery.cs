using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Queries;

public record GetFuelRecordByIdQuery(Guid Id) : IRequest<FuelRecord?>;
