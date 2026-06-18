using MediatR;

namespace FleetService.Application.Features.FuelRecords.Commands;

public record DeleteFuelRecordCommand(Guid Id) : IRequest<bool>;
