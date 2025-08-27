using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxusIntegration.Api.BackgroundServices
{
    public class SftpPollingHostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Poll SFTP for new files
            return Task.CompletedTask;
        }
    }
}