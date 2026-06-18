using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Queries;

public record GetAllDriversQuery : IRequest<IEnumerable<Driver>>;
