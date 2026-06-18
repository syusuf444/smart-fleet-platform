using MediatR;

namespace FleetService.Application.Features.Drivers.Commands;

public record DeleteDriverCommand(Guid Id) : IRequest<bool>;
