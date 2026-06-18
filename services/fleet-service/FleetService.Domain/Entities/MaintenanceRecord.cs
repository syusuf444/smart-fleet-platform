namespace FleetService.Domain.Entities;

public class MaintenanceRecord
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public string ServiceType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ScheduledDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public decimal Cost { get; set; }

    public string Vendor { get; set; } = string.Empty;

    public string Status { get; set; } = "Scheduled";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = "system";

    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public Vehicle? Vehicle { get; set; }
}
