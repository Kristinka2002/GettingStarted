using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;
        readonly ILogger<Worker> _logger;
        public Worker(IBus bus, ILogger<Worker> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               // await _bus.Publish(new Message { Text = $"The time is {DateTimeOffset.Now}" }, stoppingToken);
               // await Task.Yield();
                var keyPressed = Console.ReadKey(true);
                if (keyPressed.Key != ConsoleKey.Escape)
                {
                   
                    await _bus.Publish(new Message { Text = (keyPressed.Key.ToString()) });
                }

                _logger.LogInformation("Text from produser: {Text}", (keyPressed.Key.ToString()));
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}