namespace AiService.Application.DTOs;

public class FleetHealthAnalysisRequestDto
{
    public string VehicleSummary { get; set; } = string.Empty;

    public string MaintenanceSummary { get; set; } = string.Empty;

    public string? FuelSummary { get; set; }
}
