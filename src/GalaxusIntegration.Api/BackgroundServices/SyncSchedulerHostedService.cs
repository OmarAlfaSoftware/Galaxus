using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxusIntegration.Api.BackgroundServices
{
    public class SyncSchedulerHostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Schedule sync jobs
            return Task.CompletedTask;
        }
    }
}