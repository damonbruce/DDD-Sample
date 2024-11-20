using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace people_service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IEndpointInstance _endpointInstance;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // NServiceBus configuration for RabbitMQ
            var endpointConfiguration = new EndpointConfiguration("MyWorkerEndpoint");

            // Define RabbitMQ as the transport
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost"); // Adjust for your RabbitMQ server
            transport.UseConventionalRoutingTopology();   // Default RabbitMQ topology

            // Set up error and audit queues (you can customize these)
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            // Persistence (in-memory for simplicity)
            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

            // Enable message retry policies
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Immediate(immediate => immediate.NumberOfRetries(5));
            recoverability.Delayed(delayed => delayed.NumberOfRetries(3).TimeIncrease(TimeSpan.FromSeconds(5)));

            _endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            _logger.LogInformation("NServiceBus Endpoint started.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Waiting for messages...");

            // You can include your background logic here if needed
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _endpointInstance.Stop()
                .ConfigureAwait(false);
            _logger.LogInformation("NServiceBus Endpoint stopped.");
        }
}
