using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Queries;

public record GetAllFuelRecordsQuery : IRequest<IEnumerable<FuelRecord>>;
