namespace FleetService.Infrastructure.Messaging.Events;

public class MaintenanceCreatedEvent
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public string ServiceType { get; set; } = string.Empty;

    public DateTime ScheduledDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
