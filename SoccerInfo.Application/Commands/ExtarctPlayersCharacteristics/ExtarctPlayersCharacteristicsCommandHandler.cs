using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
internal class ExtarctPlayersCharacteristicsCommandHandler(
    ApplicationDbContext dbContext,
    PlayersCharacteristicsExtractor extractor
    ) : ICommandHandler<ExtarctPlayersCharacteristicsCommand>
{
    public async Task Handle(ExtarctPlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        var extractionInput = dbContext.Players
            .Where(x => !request.OnlyNewPlayers || x.Characteristics == null)
            .Take(request.PlayerCount)
            .Select(x => new PlayerExtractionData()
            {
                TransfermarktId = x.TransfermarktId,
                TransfermarktURL = x.TransfermarktURL,
                isGoalkeeper =  x.Position == "Goalkeeper"
            });

        var extraction = await extractor.TryExtract(extractionInput);

        if (extraction == null)
            return;

        await JsonSerializerToFile.Save(extraction, "characteristics_data_6.json");
    }
}
