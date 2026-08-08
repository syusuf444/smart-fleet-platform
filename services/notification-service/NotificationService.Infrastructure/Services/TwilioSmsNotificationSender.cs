using NotificationService.Domain.Events;
using NotificationService.Domain.Port;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace NotificationService.Infrastructure.Services;

public class TwilioSmsNotificationSender : INotificationSender
{
    private readonly SmsNotificationSettings _settings;

    public TwilioSmsNotificationSender(SmsNotificationSettings settings)
    {
        _settings = settings;
    }

    public string Channel => "sms";

    public Task SendAsync(NotificationMessage message)
    {
        if (!_settings.Enabled)
        {
            Console.WriteLine("[SMS] Twilio sender disabled in configuration.");
            return Task.CompletedTask;
        }

        if (string.IsNullOrWhiteSpace(_settings.AccountSid) || string.IsNullOrWhiteSpace(_settings.AuthToken))
        {
            throw new InvalidOperationException("Twilio SMS settings are not configured.");
        }

        TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);

        var smsMessage = MessageResource.Create(
            to: new PhoneNumber(message.RecipientPhoneNumber),
            from: new PhoneNumber(_settings.FromNumber),
            body: message.Body);

        Console.WriteLine($"[SMS] Twilio message queued: {smsMessage.Sid}");
        return Task.CompletedTask;
    }
}
