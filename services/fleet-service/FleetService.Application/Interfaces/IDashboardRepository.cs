using FleetService.Application.DTOs;

namespace FleetService.Application.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default);
}
