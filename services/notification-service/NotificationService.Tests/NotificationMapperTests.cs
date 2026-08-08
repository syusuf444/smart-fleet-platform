using NotificationService.Domain.Events;
using NotificationService.Infrastructure.Services;
using Xunit;

namespace NotificationService.Tests;

public class NotificationMapperTests
{
    [Fact]
    public void ToNotificationMessage_MapsVehicleCreatedEventToEmail()
    {
        var payload = System.Text.Json.JsonSerializer.Serialize(new VehicleCreatedEvent
        {
            Id = Guid.NewGuid(),
            VehicleNumber = "ABC123",
            Manufacturer = "Acme",
            Model = "Hauler",
            Year = 2025,
            CreatedAt = DateTime.UtcNow
        });

        var message = NotificationMapper.ToNotificationMessage("vehicle-created", payload);

        Assert.Equal("email", message.Channel);
        Assert.Contains("ABC123", message.Subject);
        Assert.Contains("Acme", message.Subject);
        Assert.Contains("Hauler", message.Body);
        Assert.Equal("ops@smartfleet.com", message.RecipientEmail);
    }

    [Fact]
    public void ToNotificationMessage_MapsMaintenanceCompletedEventToSms()
    {
        var payload = System.Text.Json.JsonSerializer.Serialize(new MaintenanceCompletedEvent
        {
            Id = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            ServiceType = "Engine Service",
            Cost = 150.5m,
            CompletedDate = DateTime.UtcNow
        });

        var message = NotificationMapper.ToNotificationMessage("maintenance.completed", payload);

        Assert.Equal("sms", message.Channel);
        Assert.Equal("+15551234567", message.RecipientPhoneNumber);
        Assert.Contains("completed", message.Subject, StringComparison.OrdinalIgnoreCase);
    }
}
