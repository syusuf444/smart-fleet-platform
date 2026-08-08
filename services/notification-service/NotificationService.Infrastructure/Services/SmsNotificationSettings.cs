namespace NotificationService.Infrastructure.Services;

public class SmsNotificationSettings
{
    public bool Enabled { get; set; } = true;
    public string AccountSid { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public string FromNumber { get; set; } = string.Empty;
}
