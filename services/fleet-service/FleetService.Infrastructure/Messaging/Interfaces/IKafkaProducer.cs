namespace FleetService.Infrastructure.Messaging.Interfaces;

public interface IKafkaProducer
{
    Task ProduceAsync<T>(string topic, T message);
}