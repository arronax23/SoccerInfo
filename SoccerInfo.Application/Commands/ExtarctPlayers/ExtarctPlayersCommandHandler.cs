using AutoMapper;
using SoccerInfo.Extractor;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtarctPlayers;

internal class ExtarctPlayersCommandHandler(
    ApplicationDbContext dbContext,
    SoccerDataExtractor soccerDataExtractor,
    IMapper mapper) : ICommandHandler<ExtarctPlayersCommand>
{
    public async Task Handle(ExtarctPlayersCommand request, CancellationToken cancellationToken)
    {
        var extraction = await soccerDataExtractor.TryExtarct();
        
        if (extraction == null)
            return;

        await JsonSerializerToFile.Save(extraction, "scraped_data_5.json");
        var extractedLeagues = mapper.Map<IEnumerable<League>>(extraction.Leagues);

        await Console.Out.WriteLineAsync();
    }
}
