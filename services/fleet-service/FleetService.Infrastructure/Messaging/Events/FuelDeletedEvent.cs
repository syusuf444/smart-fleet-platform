namespace FleetService.Infrastructure.Messaging.Events;

public class FuelDeletedEvent
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public DateTime DeletedAt { get; set; }
}
