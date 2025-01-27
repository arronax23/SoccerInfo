using Polly;

namespace SoccerInfo.FrontendScraper.Resilience;
internal static class CharacteristicsExtractionPipeline
{
    public static string Name => "CharacteristicsExtraction";

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
