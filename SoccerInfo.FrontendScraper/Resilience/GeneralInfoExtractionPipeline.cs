using Polly;

namespace SoccerInfo.FrontendScraper.Resilience;
internal static class GeneralInfoExtractionPipeline
{
    public static string Name => "GeneralInfoExtraction";

    public static void Configure(ResiliencePipelineBuilder config)
    {
        config.AddRetry(new()
        {
            Delay = TimeSpan.FromSeconds(2),
            BackoffType = DelayBackoffType.Linear,
            UseJitter = true,
            MaxRetryAttempts = 3,
        })
        .AddTimeout(TimeSpan.FromMinutes(7));
    }
}
