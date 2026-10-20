namespace AiService.Application.DTOs;

public sealed class AiActionPlanDto
{
    public string AssistantMessage { get; set; } = string.Empty;

    public string Reasoning { get; set; } = string.Empty;

    public List<ActionDraftDto> Actions { get; set; } = new();
}

public sealed class ActionDraftDto
{
    public string Action { get; set; } = "none";

    public TripActionDraftDto? Trip { get; set; }

    public MaintenanceActionDraftDto? Maintenance { get; set; }

    public FuelActionDraftDto? Fuel { get; set; }

    public VehicleStatusActionDraftDto? VehicleStatus { get; set; }

    public DriverAssignmentActionDraftDto? DriverAssignment { get; set; }
}

public sealed class TripActionDraftDto
{
    public string? VehicleNumber { get; set; }

    public string? DriverEmail { get; set; }

    public string? Source { get; set; }

    public string? Destination { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }
}

public sealed class MaintenanceActionDraftDto
{
    public string? VehicleNumber { get; set; }

    public string? ServiceType { get; set; }

    public string? Description { get; set; }

    public string? ScheduledDate { get; set; }

    public decimal? Cost { get; set; }

    public string? Vendor { get; set; }
}

public sealed class FuelActionDraftDto
{
    public string? VehicleNumber { get; set; }

    public string? FuelDate { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Cost { get; set; }

    public decimal? OdometerReading { get; set; }
}

public sealed class VehicleStatusActionDraftDto
{
    public string? VehicleNumber { get; set; }

    public string? NewStatus { get; set; }

    public string? Reason { get; set; }
}

public sealed class DriverAssignmentActionDraftDto
{
    public string? VehicleNumber { get; set; }

    public string? DriverEmail { get; set; }

    public bool Unassign { get; set; }
}
