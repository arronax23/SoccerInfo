using Quartz;
using SoccerInfo.Application.Commands.GeneralExtraction;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class GeneralExtractionJob(
    ILogger<GeneralExtractionJob> logger,
    IConfiguration configuration,
    ICommandDispatcher commandDispatcher) : IJobService
{
    public static string Name => "GeneralExtraction";
    public static JobKey Key => JobKey.Create(Name);
    public static string Schedule => "0 15 20 ? * WED";

    private readonly bool _isActive = configuration.GetValue<bool>("GeneralExtraction:IsActive");

    public async Task Execute(IJobExecutionContext context)
    {
        if (_isActive)
            await Run(context);
        else
            logger.LogInformation($"{Name} job disabled");
    }

    private async Task Run(IJobExecutionContext context)
    {
        logger.LogInformation($"{Name} job executing, Start time: {DateTime.Now.ToString()}");
        try
        {
            await commandDispatcher.Send(new GeneralExtractionCommand(), context.CancellationToken);
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
            logger.LogInformation($"{Name} job has finished, End time: {DateTime.Now.ToString()}");
        }
    }
}
