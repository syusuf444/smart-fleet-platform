using FleetService.Application.Features.FuelRecords.Commands;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Handlers;

public class CreateFuelRecordHandler
    : IRequestHandler<CreateFuelRecordCommand, FuelRecord>
{
    private readonly IFuelRecordService _service;

    public CreateFuelRecordHandler(IFuelRecordService service)
    {
        _service = service;
    }

    public async Task<FuelRecord> Handle(
        CreateFuelRecordCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.FuelRecordDto);
    }
}
