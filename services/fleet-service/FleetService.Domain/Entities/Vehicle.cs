namespace FleetService.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }

    public string VehicleNumber { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public double FuelCapacity { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}