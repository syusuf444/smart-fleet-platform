using FleetService.Application.DTOs;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.Drivers.Commands;

public record CreateDriverCommand(
    CreateDriverDto DriverDto
) : IRequest<Driver>;
