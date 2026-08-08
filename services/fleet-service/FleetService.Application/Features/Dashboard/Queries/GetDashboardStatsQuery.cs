using FleetService.Application.DTOs;
using MediatR;

namespace FleetService.Application.Features.Dashboard.Queries;

public class GetDashboardStatsQuery : IRequest<DashboardOverviewDto>
{
}
