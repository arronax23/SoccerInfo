using AutoMapper;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.JsonFileData;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctPlayers;

internal class ExtarctPlayersCommandHandler(
    PlayersGeneralInfoExtractor soccerDataExtractor,
    JsonFileDataManager jsonFileDataManager,
    IMapper mapper) : ICommandHandler<ExtarctPlayersCommand>
{
    public async Task Handle(ExtarctPlayersCommand request, CancellationToken cancellationToken)
    {
        var extraction = await soccerDataExtractor.TryExtarct();
        
        if (extraction == null)
            return;

        await jsonFileDataManager.SaveData(extraction, "players_general_info_data");
        var extractedLeagues = mapper.Map<IEnumerable<League>>(extraction.Leagues);

        await Console.Out.WriteLineAsync();
    }
}
