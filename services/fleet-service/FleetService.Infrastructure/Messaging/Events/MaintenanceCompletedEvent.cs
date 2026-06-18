namespace FleetService.Infrastructure.Messaging.Events;

public class MaintenanceCompletedEvent
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public string ServiceType { get; set; } = string.Empty;

    public DateTime CompletedDate { get; set; }

    public decimal Cost { get; set; }
}
