using FleetService.Application.Features.FuelRecords.Queries;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Handlers;

public class GetFuelRecordByIdHandler
    : IRequestHandler<GetFuelRecordByIdQuery, FuelRecord?>
{
    private readonly IFuelRecordService _service;

    public GetFuelRecordByIdHandler(IFuelRecordService service)
    {
        _service = service;
    }

    public async Task<FuelRecord?> Handle(
        GetFuelRecordByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
