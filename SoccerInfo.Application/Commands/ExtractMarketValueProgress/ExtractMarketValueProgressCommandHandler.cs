using Microsoft.EntityFrameworkCore;
using SoccerInfo.BackendScraper;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtractMarketValueProgress;

internal class ExtractMarketValueProgressCommandHandler(
    ApplicationDbContext dbContext, 
    MarketValueProgressScraper marketValueProgressScraper,
    CustomMapper mapper
    ) : ICommandHandler<ExtractMarketValueProgressCommand>
{
    public async Task Handle(ExtractMarketValueProgressCommand request, CancellationToken cancellationToken)
    {
        var players = dbContext.Players;
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

        dbContext.UpdateRange(players);

        var entries = dbContext.ChangeTracker.Entries();

        await dbContext.SaveChangesAsync();

        await Console.Out.WriteLineAsync();
    }
}
