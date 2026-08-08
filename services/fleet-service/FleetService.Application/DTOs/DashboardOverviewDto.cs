namespace FleetService.Application.DTOs;

public class DashboardOverviewDto
{
    public DashboardStatsDto Stats { get; set; } = new();

    public List<VehicleStatusCountDto> VehicleStatusBreakdown { get; set; } = [];

    public List<MonthlyCostDto> MonthlyFuelCosts { get; set; } = [];

    public List<RecentActivityDto> RecentActivities { get; set; } = [];
}

public class VehicleStatusCountDto
{
    public string Status { get; set; } = string.Empty;

    public int Count { get; set; }
}

public class MonthlyCostDto
{
    public string Month { get; set; } = string.Empty;

    public double Cost { get; set; }
}

public class RecentActivityDto
{
    public string Id { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime OccurredAt { get; set; }
}
