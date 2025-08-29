using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.BackendScraper.MarketValueProgress;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtractMarketValueProgress;

internal class ExtractMarketValueProgressCommandHandler(
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository, 
    MarketValueProgressScraper marketValueProgressScraper,
    CustomMapper mapper
    ) : ICommandHandler<ExtractMarketValueProgressCommand>
{
    public async Task Handle(ExtractMarketValueProgressCommand request, CancellationToken cancellationToken)
    {
        var players = playerRepository.ToQuery();
        var extractedData = await Benchmark.ExecuteAndMeasureTimeAsync(async () => {
            return await marketValueProgressScraper.Scrape(players.Select(x => x.TransfermarktId));
        }, "Scrape Market Value Progress");

        foreach (var player in players)
        {
            var playerProgressExtarcted = extractedData.SingleOrDefault(x => x.PlayerTransfermarktId == player.TransfermarktId);

            if (playerProgressExtarcted != null)
            {
                var marketValueChanges = mapper.Map(playerProgressExtarcted);

                player.AddNewMarketValueChanges(marketValueChanges);
            }  
        }

        playerRepository.UpdateRange(players);

        var entries = unitOfWork.GetEntires();

        await unitOfWork.SaveChangesAsync();
        await Console.Out.WriteLineAsync();
    }
}
