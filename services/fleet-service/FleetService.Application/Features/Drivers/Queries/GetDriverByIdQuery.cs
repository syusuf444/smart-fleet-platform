using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Queries;

public record GetDriverByIdQuery(Guid Id) : IRequest<Driver?>;
