using System.Net;
using System.Net.Mail;
using NotificationService.Domain.Events;
using NotificationService.Domain.Port;

namespace NotificationService.Infrastructure.Services;

public class SmtpEmailNotificationSender : INotificationSender
{
    private readonly EmailNotificationSettings _settings;

    public SmtpEmailNotificationSender(EmailNotificationSettings settings)
    {
        _settings = settings;
    }

    public string Channel => "email";

    public async Task SendAsync(NotificationMessage message)
    {
        if (!_settings.Enabled)
        {
            Console.WriteLine("[Email] SMTP sender disabled in configuration.");
            return;
        }

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_settings.FromAddress, _settings.FromDisplayName),
            Subject = message.Subject,
            Body = message.Body,
            IsBodyHtml = false
        };

        mailMessage.To.Add(message.RecipientEmail);

        using var smtpClient = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            UseDefaultCredentials = _settings.UseDefaultCredentials,
            Credentials = _settings.UseDefaultCredentials ? CredentialCache.DefaultNetworkCredentials : new NetworkCredential(_settings.UserName, _settings.Password)
        };

        await smtpClient.SendMailAsync(mailMessage);
        Console.WriteLine($"[Email] Sent to {message.RecipientEmail}");
    }
}
