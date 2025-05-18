using Polly;

namespace SoccerInfo.Application.Commands.ExtractNationalities;
internal class NationalityExtractionPipeline
{
    public static string Name => "NationalityExtraction";

    public static void Configure(ResiliencePipelineBuilder config)
    {
        config.AddRetry(new()
        {
            Delay = TimeSpan.FromMinutes(1),
            BackoffType = DelayBackoffType.Linear,
            UseJitter = true,
            MaxRetryAttempts = 4,
        })
        .AddTimeout(TimeSpan.FromMinutes(15));
    }
}
