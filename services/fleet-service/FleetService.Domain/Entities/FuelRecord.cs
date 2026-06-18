namespace FleetService.Domain.Entities;

public class FuelRecord
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public DateTime FuelDate { get; set; }

    public decimal Quantity { get; set; }

    public decimal Cost { get; set; }

    public decimal OdometerReading { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = "system";

    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public Vehicle? Vehicle { get; set; }
}
