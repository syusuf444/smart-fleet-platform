namespace NotificationService.Infrastructure.Messaging.Interfaces;

public interface INotificationProducer
{
    Task ProduceAsync<T>(string topic, T message);
}
