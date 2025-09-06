using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Services;
using SoccerInfo.BackendScraper.MarketValueProgress;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;
using static SoccerInfo.BackendScraper.MarketValueProgress.MarketValueProgressScraper;
using static SoccerInfo.Domain.Models.Extraction.ExtractionInfo;

namespace SoccerInfo.Application.Commands.ExtractMarketValueProgress;

internal class ExtractMarketValueProgressCommandHandler(
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository, 
    MarketValueProgressScraper marketValueProgressScraper,
    ExtractionInfoService extractionInfoService,
    CustomMapper mapper
    ) : ICommandHandler<ExtractMarketValueProgressCommand>
{
    public async Task Handle(ExtractMarketValueProgressCommand request, CancellationToken cancellationToken)
    {
        var players = playerRepository.ToQuery();

        var extractionData = await extractionInfoService.Use(async () =>
        {
            return await marketValueProgressScraper.Scrape(players.Select(x => x.TransfermarktId));
        },
        ExtractionType.MarketValueProgress, saveToFile: false);

        await Save(players, extractionData);
    }

    private async Task Save(IQueryable<Player> players, IEnumerable<MarketValueProgressData> extractionData) 
    {
        foreach (var player in players)
        {
            var playerProgressExtarcted = extractionData.SingleOrDefault(x => x.PlayerTransfermarktId == player.TransfermarktId);

            if (playerProgressExtarcted != null)
            {
                var marketValueChanges = mapper.Map(playerProgressExtarcted);

                player.AddNewMarketValueChanges(marketValueChanges);
            }
        }

        await unitOfWork.SaveChangesAsync();
    }
}
