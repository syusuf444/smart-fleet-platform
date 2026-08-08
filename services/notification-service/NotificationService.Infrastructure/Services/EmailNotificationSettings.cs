namespace NotificationService.Infrastructure.Services;

public class EmailNotificationSettings
{
    public bool Enabled { get; set; } = true;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public bool EnableSsl { get; set; } = true;
    public string FromAddress { get; set; } = "no-reply@smartfleet.com";
    public string FromDisplayName { get; set; } = "Smart Fleet";
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool UseDefaultCredentials { get; set; }
}
