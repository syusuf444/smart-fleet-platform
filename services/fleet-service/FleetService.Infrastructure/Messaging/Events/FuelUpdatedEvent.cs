namespace FleetService.Infrastructure.Messaging.Events;

public class FuelUpdatedEvent
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Cost { get; set; }

    public DateTime UpdatedAt { get; set; }
}
