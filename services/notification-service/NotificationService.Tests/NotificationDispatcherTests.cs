using NotificationService.Domain.Events;
using NotificationService.Domain.Port;
using NotificationService.Infrastructure.Services;
using Xunit;

namespace NotificationService.Tests;

public class NotificationDispatcherTests
{
    [Fact]
    public async Task DispatchAsync_UsesCorrectSenderBasedOnChannel()
    {
        var emailSender = new TestNotificationSender("email");
        var smsSender = new TestNotificationSender("sms");
        var dispatcher = new NotificationDispatcher(new[] { emailSender, smsSender });

        var message = new NotificationMessage
        {
            Channel = "sms",
            Body = "Test SMS",
            RecipientPhoneNumber = "+15550000000"
        };

        await dispatcher.DispatchAsync(message);

        Assert.False(emailSender.Sent);
        Assert.True(smsSender.Sent);
    }

    private class TestNotificationSender : INotificationSender
    {
        public string Channel { get; }
        public bool Sent { get; private set; }

        public TestNotificationSender(string channel)
        {
            Channel = channel;
        }

        public Task SendAsync(NotificationMessage message)
        {
            Sent = true;
            return Task.CompletedTask;
        }
    }
}
