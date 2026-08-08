using MediatR;

namespace AIService.Application.Features.Maintenance.Queries;

public class GetPredictiveMaintenanceQuery : IRequest<string>
{
    public Guid VehicleId { get; set; }
}