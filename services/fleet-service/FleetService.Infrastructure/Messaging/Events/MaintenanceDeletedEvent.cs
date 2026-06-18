namespace FleetService.Infrastructure.Messaging.Events;

public class MaintenanceDeletedEvent
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public DateTime DeletedAt { get; set; }
}
