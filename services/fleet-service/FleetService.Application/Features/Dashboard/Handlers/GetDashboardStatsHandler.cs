using FleetService.Application.DTOs;
using FleetService.Application.Features.Dashboard.Queries;
using FleetService.Application.Interfaces;
using MediatR;

namespace FleetService.Application.Features.Dashboard.Handlers;

public class GetDashboardStatsHandler
    : IRequestHandler<GetDashboardStatsQuery, DashboardOverviewDto>
{
    private readonly IDashboardRepository _dashboardRepository;

    public GetDashboardStatsHandler(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardOverviewDto> Handle(
        GetDashboardStatsQuery request,
        CancellationToken cancellationToken)
    {
        return await _dashboardRepository.GetOverviewAsync(cancellationToken);
    }
}
