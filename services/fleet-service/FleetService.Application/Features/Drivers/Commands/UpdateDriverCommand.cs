using FleetService.Application.DTOs;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Commands;

public record UpdateDriverCommand(
    Guid Id,
    UpdateDriverDto DriverDto
) : IRequest<Driver?>;
