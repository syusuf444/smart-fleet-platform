namespace FleetService.Infrastructure.Messaging.Events;

public class FuelAddedEvent
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public DateTime FuelDate { get; set; }

    public decimal Quantity { get; set; }

    public decimal Cost { get; set; }

    public decimal OdometerReading { get; set; }

    public DateTime CreatedAt { get; set; }
}
