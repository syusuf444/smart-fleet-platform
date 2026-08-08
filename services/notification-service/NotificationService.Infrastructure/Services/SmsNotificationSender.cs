using NotificationService.Domain.Events;
using NotificationService.Domain.Port;

namespace NotificationService.Infrastructure.Services;

public class SmsNotificationSender : INotificationSender
{
    public string Channel => "sms";

    public Task SendAsync(NotificationMessage message)
    {
        // TODO: wire to SMS provider
        Console.WriteLine($"[SMS] To={message.RecipientPhoneNumber}, Body={message.Body}");
        return Task.CompletedTask;
    }
}
