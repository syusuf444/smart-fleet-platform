using NotificationService.Domain.Events;
using NotificationService.Domain.Port;

namespace NotificationService.Infrastructure.Services;

public class EmailNotificationSender : INotificationSender
{
    public string Channel => "email";

    public Task SendAsync(NotificationMessage message)
    {
        // TODO: wire to SMTP or email service provider
        Console.WriteLine($"[Email] To={message.RecipientEmail}, Subject={message.Subject}");
        return Task.CompletedTask;
    }
}
