using Microsoft.Extensions.Logging;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.JsonData;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
internal class ExtarctPlayersCharacteristicsCommandHandler(
    ILogger<ExtarctPlayersCharacteristicsCommandHandler> logger,
    JsonDataManager jsonDataManager,
    ApplicationDbContext dbContext,
    PlayersCharacteristicsExtractor extractor
    ) : ICommandHandler<ExtarctPlayersCharacteristicsCommand>
{
    public async Task Handle(ExtarctPlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        await Benchmark.ExecuteAndMeasureTimeAsync(async () =>
        {
            var extractionInput = dbContext.Players
            .Where(x => !request.OnlyNewPlayers || x.Characteristics == null)
            .Take(request.PlayerCount)
            .Select(x => new PlayerExtractionData()
            {
                TransfermarktId = x.TransfermarktId,
                TransfermarktURL = x.TransfermarktURL,
                isGoalkeeper = x.Position == "Goalkeeper"
            })
            .ToList();

            var extraction = await extractor.TryExtract(extractionInput, cancellationToken);

            if (extraction == null)
                return;

            await jsonDataManager.SaveData(extraction, "characteristics_data");
        }, "ExtarctPlayersCharacteristicsCommand");

        logger.LogInformation("ExtarctPlayersCharacteristicsCommand has finished");

    }
}
