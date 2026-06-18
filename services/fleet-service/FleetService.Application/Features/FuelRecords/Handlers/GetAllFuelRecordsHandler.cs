using FleetService.Application.Features.FuelRecords.Queries;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Handlers;

public class GetAllFuelRecordsHandler
    : IRequestHandler<GetAllFuelRecordsQuery, IEnumerable<FuelRecord>>
{
    private readonly IFuelRecordService _service;

    public GetAllFuelRecordsHandler(IFuelRecordService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<FuelRecord>> Handle(
        GetAllFuelRecordsQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync();
    }
}
