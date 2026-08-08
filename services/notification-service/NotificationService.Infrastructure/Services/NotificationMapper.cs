using NotificationService.Domain.Events;

namespace NotificationService.Infrastructure.Services;

public static class NotificationMapper
{
    public static NotificationMessage ToNotificationMessage(string topic, string payload)
    {
        return topic switch
        {
            "vehicle-created" => MapVehicleCreated(payload),
            "maintenance.created" => MapMaintenanceCreated(payload),
            "maintenance.completed" => MapMaintenanceCompleted(payload),
            _ => throw new ArgumentException($"Unsupported topic '{topic}'", nameof(topic))
        };
    }

    private static NotificationMessage MapVehicleCreated(string payload)
    {
        var @event = System.Text.Json.JsonSerializer.Deserialize<VehicleCreatedEvent>(payload);

        return new NotificationMessage
        {
            Channel = "email",
            RecipientEmail = "ops@smartfleet.com",
            Subject = $"New vehicle created: {@event?.VehicleNumber} ({@event?.Manufacturer} {@event?.Model})",
            Body = $"Vehicle {@event?.VehicleNumber} ({@event?.Manufacturer} {@event?.Model}, {@event?.Year}) was created at {@event?.CreatedAt:u}."
        };
    }

    private static NotificationMessage MapMaintenanceCreated(string payload)
    {
        var @event = System.Text.Json.JsonSerializer.Deserialize<MaintenanceCreatedEvent>(payload);

        return new NotificationMessage
        {
            Channel = "email",
            RecipientEmail = "maintenance@smartfleet.com",
            Subject = $"Maintenance scheduled for vehicle {@event?.VehicleId}",
            Body = $"Maintenance {@event?.ServiceType} scheduled for {@event?.ScheduledDate:u} with status {@event?.Status} on vehicle {@event?.VehicleId}."
        };
    }

    private static NotificationMessage MapMaintenanceCompleted(string payload)
    {
        var @event = System.Text.Json.JsonSerializer.Deserialize<MaintenanceCompletedEvent>(payload);

        return new NotificationMessage
        {
            Channel = "sms",
            RecipientPhoneNumber = "+15551234567",
            Subject = "Maintenance completed",
            Body = $"Maintenance {@event?.Id} completed for vehicle {@event?.VehicleId} at {@event?.CompletedDate:u}. Total cost: {@event?.Cost:C}."
        };
    }
}
