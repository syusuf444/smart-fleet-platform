namespace NotificationService.Domain.Events;

public class NotificationMessage
{
    public string Channel { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
    public string RecipientPhoneNumber { get; set; } = string.Empty;
}

public class VehicleCreatedEvent
{
    public Guid Id { get; set; }
    public string VehicleNumber { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class MaintenanceCreatedEvent
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class MaintenanceCompletedEvent
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public DateTime CompletedDate { get; set; }
    public decimal Cost { get; set; }
}
