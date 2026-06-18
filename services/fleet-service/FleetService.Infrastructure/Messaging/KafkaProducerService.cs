using Confluent.Kafka;
using FleetService.Infrastructure.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FleetService.Infrastructure.Messaging;

public class KafkaProducerService : IKafkaProducer
{
    private readonly string _bootstrapServers;

    public KafkaProducerService(IConfiguration configuration)
    {
        _bootstrapServers =
            configuration["Kafka:BootstrapServers"]!;
    }

    public async Task ProduceAsync<T>(
        string topic,
        T message)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _bootstrapServers
        };

        using var producer =
            new ProducerBuilder<Null, string>(config)
                .Build();

        var jsonMessage =
            JsonSerializer.Serialize(message);

        await producer.ProduceAsync(
            topic,
            new Message<Null, string>
            {
                Value = jsonMessage
            });

        producer.Flush(TimeSpan.FromSeconds(5));
    }
}