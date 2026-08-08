using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using NotificationService.Infrastructure.Messaging.Interfaces;
using System.Text.Json;

namespace NotificationService.Infrastructure.Messaging;

public class NotificationProducerService : INotificationProducer
{
    private readonly string _bootstrapServers;

    public NotificationProducerService(IConfiguration configuration)
    {
        _bootstrapServers = configuration["Kafka:BootstrapServers"]!;
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _bootstrapServers
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var jsonMessage = JsonSerializer.Serialize(message);

        await producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonMessage });
        producer.Flush(TimeSpan.FromSeconds(5));
    }
}
