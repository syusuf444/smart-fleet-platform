namespace AIService.Application.Interfaces;

public interface IFleetDataClient
{
    Task<string> GetFleetSummaryAsync();
    Task<string> GetMaintenanceHistoryAsync(Guid vehicleId);
    Task<string> GetVehicleDetailsAsync(Guid vehicleId);
    Task<string> GetActiveTripsAsync();
}