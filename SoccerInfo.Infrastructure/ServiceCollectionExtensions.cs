using Quartz;
using SoccerInfo.Infrastructure.Jobs;

namespace SoccerInfo.Infrastructure;
static class ServiceCollectionExtensions
{
    public static void RegisterMediatR(this IServiceCollection services)
    {
        foreach (var marker in GetAssemblyMarkers())
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(marker));
    }

    public static void AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(cfg =>
        {
            cfg.AddJob<GeneralExtractionJob>(GeneralExtractionJob.Key)
                .AddTrigger(trigger =>
                    trigger.ForJob(GeneralExtractionJob.Key)
                           .WithCronSchedule("0 0 23 * * ?"));
        });

        services.AddQuartzHostedService(cfg => cfg.WaitForJobsToComplete = true);
    }

    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(GetAssemblyMarkers());
    }

    private static Type[] GetAssemblyMarkers()
    {
        return [
            typeof(SoccerInfo.API.IAssemblyMarker),
            typeof(SoccerInfo.Application.IAssemblyMarker),
            typeof(SoccerInfo.FrontendScraper.IAssemblyMarker),
            typeof(SoccerInfo.BackendScraper.IAssemblyMarker),
            typeof(SoccerInfo.Persistence.IAssemblyMarker),
            typeof(SoccerInfo.Infrastructure.IAssemblyMarker),
        ];
    }
}
