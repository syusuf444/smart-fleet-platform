namespace FleetService.Infrastructure.Messaging.Events;

public class DriverDeletedEvent
{
    public Guid Id { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public DateTime DeletedAt { get; set; }
}
