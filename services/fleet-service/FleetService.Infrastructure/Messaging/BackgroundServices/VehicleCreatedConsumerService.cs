using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FleetService.Infrastructure.Messaging.BackgroundServices;

public class VehicleCreatedConsumerService
    : BackgroundService
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<
        VehicleCreatedConsumerService> _logger;

    public VehicleCreatedConsumerService(
        IConfiguration configuration,
        ILogger<VehicleCreatedConsumerService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers =
                _configuration["Kafka:BootstrapServers"],

            GroupId = "fleet-group",

            AutoOffsetReset =
                AutoOffsetReset.Earliest,

            AllowAutoCreateTopics = true
        };

        using var consumer =
            new ConsumerBuilder<Ignore, string>(config)
                .Build();

        consumer.Subscribe("vehicle-created");

        _logger.LogInformation(
            "Kafka Consumer Started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult =
                    consumer.Consume(
                        TimeSpan.FromSeconds(1));

                if (consumeResult != null)
                {
                    _logger.LogInformation(
                        $"Kafka Event Received: {consumeResult.Message.Value}");
                }
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(
                    ex,
                    "Kafka consume error");

                await Task.Delay(
                    5000,
                    stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected Kafka error");

                await Task.Delay(
                    5000,
                    stoppingToken);
            }
        }
    }
}