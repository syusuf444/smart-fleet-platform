namespace FleetService.Application.DTOs;

public class DashboardStatsDto
{
    public int TotalVehicles { get; set; }
    public int ActiveTrips { get; set; }
    public int AvailableDrivers { get; set; }
    public int MaintenanceDueCount { get; set; }
    public double MonthlyFuelCost { get; set; }
    public int SafetyScore { get; set; } // Mocked for Stitch AI design
}