using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Vehicles.Queries;

public record GetAllVehiclesQuery : IRequest<IEnumerable<Vehicle>>;