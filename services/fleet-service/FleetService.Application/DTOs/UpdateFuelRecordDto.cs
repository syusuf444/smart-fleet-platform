namespace FleetService.Application.DTOs;

public class UpdateFuelRecordDto
{
    public Guid VehicleId { get; set; }

    public DateTime FuelDate { get; set; }

    public decimal Quantity { get; set; }

    public decimal Cost { get; set; }

    public decimal OdometerReading { get; set; }
}
