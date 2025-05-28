using Polly;

namespace SoccerInfo.FrontendScraper.Resilience;
internal static class GeneralInfoExtractionPipeline
{
    public static string Name => "GeneralInfoExtraction";

    public static void Configure(ResiliencePipelineBuilder config)
    {
        config.AddRetry(new()
        {
            Delay = TimeSpan.FromSeconds(30),
            BackoffType = DelayBackoffType.Linear,
            UseJitter = true,
            MaxRetryAttempts = 5,
        })
        .AddTimeout(TimeSpan.FromMinutes(7));
    }
}
