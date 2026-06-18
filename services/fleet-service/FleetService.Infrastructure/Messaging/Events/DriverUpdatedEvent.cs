namespace FleetService.Infrastructure.Messaging.Events;

public class DriverUpdatedEvent
{
    public Guid Id { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; }
}
