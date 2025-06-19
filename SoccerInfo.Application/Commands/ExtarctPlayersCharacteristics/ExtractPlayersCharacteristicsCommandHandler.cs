using Microsoft.Extensions.Logging;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.JsonFileData;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
internal class ExtractPlayersCharacteristicsCommandHandler(
    ILogger<ExtractPlayersCharacteristicsCommandHandler> logger,
    JsonFileDataManager jsonFileDataManager,
    ApplicationDbContext dbContext,
    PlayersCharacteristicsExtractor extractor
    ) : ICommandHandler<ExtractPlayersCharacteristicsCommand, PlayersCharacteristicsExtractionData?>
{
    public async Task<PlayersCharacteristicsExtractionData?> Handle(ExtractPlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        var extraction = await Benchmark.ExecuteAndMeasureTimeAsync(async () =>
        {
            var extractionInput = dbContext.Players
            .Where(x => !request.OnlyNewPlayers || x.Characteristics == null)
            .Skip(request.Skip)
            .Take(request.PlayerCount)
            .Select(x => new PlayerExtractionData()
            {
                TransfermarktId = x.TransfermarktId,
                TransfermarktURL = x.TransfermarktURL,
                isGoalkeeper = x.Position == "Goalkeeper"
            })
            .ToList();

            var extraction = await extractor.TryExtract(extractionInput, cancellationToken);

            if (extraction != null)
                await jsonFileDataManager.SaveData(extraction, "characteristics_data");

            return extraction;
        }, "ExtarctPlayersCharacteristicsCommand");

        logger.LogInformation("ExtarctPlayersCharacteristicsCommand has finished");

        return extraction;

    }
}
