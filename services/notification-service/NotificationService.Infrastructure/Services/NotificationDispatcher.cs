using NotificationService.Domain.Events;
using NotificationService.Domain.Port;

namespace NotificationService.Infrastructure.Services;

public class NotificationDispatcher
{
    private readonly IEnumerable<INotificationSender> _senders;

    public NotificationDispatcher(IEnumerable<INotificationSender> senders)
    {
        _senders = senders;
    }

    public async Task DispatchAsync(NotificationMessage message)
    {
        var tasks = _senders
            .Where(sender => sender.Channel == message.Channel)
            .Select(sender => sender.SendAsync(message));

        if (!tasks.Any())
        {
            throw new InvalidOperationException($"No notification sender configured for channel '{message.Channel}'");
        }

        await Task.WhenAll(tasks);
    }
}
