namespace FleetService.Infrastructure.Messaging.Events;

public class VehicleCreatedEvent
{
    public Guid Id { get; set; }

    public string VehicleNumber { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public DateTime CreatedAt { get; set; }
}