using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Commands.ExtractBackend;
using SoccerInfo.BackendScraper;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtarctBackend;

internal class ExtarctBackendCommandHandler(
    ApplicationDbContext dbContext, 
    MarketValueProgressScraper marketValueProgressScraper,
    CustomMapper mapper
    ) : ICommandHandler<ExtarctBackendCommand>
{
    public async Task Handle(ExtarctBackendCommand request, CancellationToken cancellationToken)
    {
        var players = dbContext.Players.Include(x => x.MarketValueProgress);
        var extractedData = await Benchmark.ExecuteAndMeasureTimeAsync(async () => {
            return await marketValueProgressScraper.Scrape(players.Select(x => x.TransfermarktId));
        }, "Scrape All");


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
