using AIService.Application.Interfaces;
using System.Net.Http.Json;

namespace AIService.Infrastructure.Clients;

public class FleetDataClient : IFleetDataClient
{
    private readonly HttpClient _httpClient;

    public FleetDataClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetFleetSummaryAsync()
    {
        // In a real implementation, this would call the FleetService API
        // and return a JSON summary of all vehicles and their status.
        var response = await _httpClient.GetAsync("api/vehicles/summary");
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetMaintenanceHistoryAsync(Guid vehicleId) => 
        await _httpClient.GetStringAsync($"api/maintenance/vehicle/{vehicleId}");

    public async Task<string> GetVehicleDetailsAsync(Guid vehicleId) => 
        await _httpClient.GetStringAsync($"api/vehicles/{vehicleId}");

    public async Task<string> GetActiveTripsAsync() => 
        await _httpClient.GetStringAsync("api/trips/active");
}