using NotificationService.Infrastructure.Services;
using Xunit;

namespace NotificationService.Tests;

public class NotificationSettingsValidatorTests
{
    [Fact]
    public void ValidateEmail_ReturnsFalse_WhenEnabledAndMissingCredentials()
    {
        var settings = new EmailNotificationSettings
        {
            Enabled = true,
            Host = "smtp.example.com",
            FromAddress = "no-reply@smartfleet.com",
            FromDisplayName = "Smart Fleet",
            UseDefaultCredentials = false,
            UserName = string.Empty,
            Password = string.Empty
        };

        var isValid = NotificationSettingsValidator.ValidateEmail(settings);

        Assert.False(isValid);
    }

    [Fact]
    public void ValidateEmail_ReturnsTrue_WhenDisabled()
    {
        var settings = new EmailNotificationSettings
        {
            Enabled = false
        };

        var isValid = NotificationSettingsValidator.ValidateEmail(settings);

        Assert.True(isValid);
    }

    [Fact]
    public void ValidateSms_ReturnsFalse_WhenEnabledAndMissingTwilioSettings()
    {
        var settings = new SmsNotificationSettings
        {
            Enabled = true,
            AccountSid = string.Empty,
            AuthToken = string.Empty,
            FromNumber = string.Empty
        };

        var isValid = NotificationSettingsValidator.ValidateSms(settings);

        Assert.False(isValid);
    }

    [Fact]
    public void ValidateSms_ReturnsTrue_WhenDisabled()
    {
        var settings = new SmsNotificationSettings
        {
            Enabled = false
        };

        var isValid = NotificationSettingsValidator.ValidateSms(settings);

        Assert.True(isValid);
    }
}
