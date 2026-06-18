using FleetService.Application.Features.FuelRecords.Commands;
using FleetService.Application.Services;
using FleetService.Domain.Entities;
using MediatR;

namespace FleetService.Application.Features.FuelRecords.Handlers;

public class UpdateFuelRecordHandler
    : IRequestHandler<UpdateFuelRecordCommand, FuelRecord?>
{
    private readonly IFuelRecordService _service;

    public UpdateFuelRecordHandler(IFuelRecordService service)
    {
        _service = service;
    }

    public async Task<FuelRecord?> Handle(
        UpdateFuelRecordCommand request,
        CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(
            request.Id,
            request.FuelRecordDto);
    }
}
