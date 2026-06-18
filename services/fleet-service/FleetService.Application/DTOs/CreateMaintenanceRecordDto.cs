namespace FleetService.Application.DTOs;

public class CreateMaintenanceRecordDto
{
    public Guid VehicleId { get; set; }

    public string ServiceType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ScheduledDate { get; set; }

    public decimal Cost { get; set; }

    public string Vendor { get; set; } = string.Empty;

    public string Status { get; set; } = "Scheduled";
}
