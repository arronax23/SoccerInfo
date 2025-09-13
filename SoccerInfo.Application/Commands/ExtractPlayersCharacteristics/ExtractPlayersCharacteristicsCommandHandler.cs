using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Services;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Domain.Models.Extraction.ExtractionInfo;

namespace SoccerInfo.Application.Commands.ExtractPlayersCharacteristics;
internal class ExtractPlayersCharacteristicsCommandHandler(
    ILogger<ExtractPlayersCharacteristicsCommandHandler> logger,
    IPlayerRepository playerRepository,
    ExtractionInfoService extractionInfoService,
    PlayersCharacteristicsExtractor extractor
    ) : ICommandHandler<ExtractPlayersCharacteristicsCommand, PlayersCharacteristicsExtractionData?>
{
    public async Task<PlayersCharacteristicsExtractionData?> Handle(ExtractPlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        var extractionInput = playerRepository
        .ToQuery()
        .Where(x => request.PlayersIds.Contains(x.Id))
        .OrderBy(x => x.Id)
        .Select(x => new PlayerExtractionData()
        {
            TransfermarktId = x.TransfermarktId,
            TransfermarktURL = x.TransfermarktURL,
            isGoalkeeper = x.Position == "Goalkeeper"
        })
        .ToList();

        var extractionData = await extractionInfoService.Use(async () =>
        {
            return await extractor.TryExtract(extractionInput, cancellationToken);
        },
        ExtractionType.PlayersCharacteristics, saveToFile: true);

        logger.LogInformation("ExtarctPlayersCharacteristicsCommand has finished");

        return extractionData;

    }
}
