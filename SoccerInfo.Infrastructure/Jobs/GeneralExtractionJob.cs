
using SoccerInfo.Application.Commands.GeneralExtraction;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Infrastructure.Jobs;

public class GeneralExtractionJob(
    ILogger<GeneralExtractionJob> logger,
    ICommandDispatcher commandDispatcher) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRunTime = DateTime.Today.AddHours(23);

            if (now > nextRunTime)
                nextRunTime = nextRunTime.AddDays(1);

            var delay = nextRunTime - now;

            try
            {
                await Task.Delay(delay, stoppingToken);
                await commandDispatcher.Send(new GeneralExtractionCommand());
            }
            catch (TaskCanceledException ex)
            {
                logger.LogError(ex.ToString());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }
}
