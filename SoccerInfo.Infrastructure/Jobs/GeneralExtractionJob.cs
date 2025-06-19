using Quartz;
using SoccerInfo.Application.Commands.GeneralExtraction;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Infrastructure.Jobs;

public class GeneralExtractionJob(
    ILogger<GeneralExtractionJob> logger,
    ICommandDispatcher commandDispatcher) : IJobService
{
    public static string Name => "GeneralExtraction";
    public static JobKey Key => JobKey.Create(Name);
    public static string Schedule => "0 25 20 * * ?";

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation($"{Name} job executing, Start time: {DateTime.UtcNow.ToString()}");

        try
        {
            await commandDispatcher.Send(new GeneralExtractionCommand());
        }
        catch (TaskCanceledException ex)
        {
            logger.LogError("Task was cancelled");
            logger.LogError(ex.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
        }
        finally
        {
            logger.LogInformation($"{Name} job has finished, End time: {DateTime.UtcNow.ToString()}");
        }
    }
}
