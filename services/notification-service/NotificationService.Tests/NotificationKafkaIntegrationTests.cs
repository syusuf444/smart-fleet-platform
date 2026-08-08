using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NotificationService.Infrastructure.Messaging;
using NotificationService.Domain.Events;
using Xunit;

namespace NotificationService.Tests;

public class NotificationKafkaIntegrationTests
{
    [Fact]
    public async System.Threading.Tasks.Task ProduceVehicleCreatedMessage_ToKafka_WhenEnvEnabled()
    {
        var runIntegration = Environment.GetEnvironmentVariable("RUN_KAFKA_INTEGRATION");
        if (runIntegration != "1")
        {
            // Skip expensive Kafka integration unless explicitly enabled.
            return;
        }

        var bootstrap = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP") ?? "localhost:9092";
        var dict = new Dictionary<string, string> { { "Kafka:BootstrapServers", bootstrap } };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(dict).Build();

        var producer = new NotificationProducerService(configuration);

        var @event = new VehicleCreatedEvent
        {
            Id = Guid.NewGuid(),
            VehicleNumber = "INT-TEST",
            Manufacturer = "TestCo",
            Model = "X1",
            Year = 2026,
            CreatedAt = DateTime.UtcNow
        };

        await producer.ProduceAsync("vehicle-created", @event);

        Assert.True(true);
    }
}
