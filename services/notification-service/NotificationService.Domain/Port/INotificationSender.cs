using NotificationService.Domain.Events;

namespace NotificationService.Domain.Port;

public interface INotificationSender
{
    string Channel { get; }

    Task SendAsync(NotificationMessage message);
}
