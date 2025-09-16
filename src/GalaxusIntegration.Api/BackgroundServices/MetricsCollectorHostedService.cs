using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxusIntegration.Api.BackgroundServices
{
    public class MetricsCollectorHostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Collect and report metrics
            return Task.CompletedTask;
        }
    }
}