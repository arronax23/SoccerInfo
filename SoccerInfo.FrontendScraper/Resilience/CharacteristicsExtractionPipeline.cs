using Polly;

namespace SoccerInfo.FrontendScraper.Resilience;
internal static class CharacteristicsExtractionPipeline
{
    public static string Name => "CharacteristicsExtraction";

    public static void Configure(ResiliencePipelineBuilder config)
    {
        config.AddRetry(new()
        {
            Delay = TimeSpan.FromMinutes(3),
            BackoffType = DelayBackoffType.Linear,
            UseJitter = true,
            MaxRetryAttempts = 8,
        })
        .AddTimeout(TimeSpan.FromMinutes(30));
    }
}
