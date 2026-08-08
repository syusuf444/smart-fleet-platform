namespace NotificationService.Infrastructure.Services;

public static class NotificationSettingsValidator
{
    public static bool ValidateEmail(EmailNotificationSettings settings)
    {
        if (!settings.Enabled)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(settings.Host) ||
            string.IsNullOrWhiteSpace(settings.FromAddress) ||
            string.IsNullOrWhiteSpace(settings.FromDisplayName))
        {
            return false;
        }

        if (!settings.UseDefaultCredentials)
        {
            return !string.IsNullOrWhiteSpace(settings.UserName) &&
                   !string.IsNullOrWhiteSpace(settings.Password);
        }

        return true;
    }

    public static bool ValidateSms(SmsNotificationSettings settings)
    {
        if (!settings.Enabled)
        {
            return true;
        }

        return !string.IsNullOrWhiteSpace(settings.AccountSid) &&
               !string.IsNullOrWhiteSpace(settings.AuthToken) &&
               !string.IsNullOrWhiteSpace(settings.FromNumber);
    }
}
