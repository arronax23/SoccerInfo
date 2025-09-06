using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Services;
using SoccerInfo.Domain.Models.Extraction;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Domain.Models.Extraction.ExtractionInfo;

namespace SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
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
        .OrderByDescending(x => x.Id)
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

        var extractionData = await extractionInfoService.Use(async () =>
        {
            return await extractor.TryExtract(extractionInput, cancellationToken);
        }, 
        ExtractionType.PlayersCharacteristics, saveToFile: true);

        logger.LogInformation("ExtarctPlayersCharacteristicsCommand has finished");

        return extractionData;

    }
}
