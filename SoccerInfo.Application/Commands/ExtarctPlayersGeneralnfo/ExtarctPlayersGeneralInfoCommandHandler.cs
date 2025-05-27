using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.JsonFileData;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;

internal class ExtarctPlayersGeneralInfoCommandHandler(
    ApplicationDbContext dbContext,
    PlayersGeneralInfoExtractor playersGeneralInfoExtractor,
    JsonFileDataManager jsonFileDataManager,
    IMapper mapper) : ICommandHandler<ExtarctPlayersGeneralnfoCommand>
{
    public async Task Handle(ExtarctPlayersGeneralnfoCommand request, CancellationToken cancellationToken)
    {
        var leagueLinks = dbContext.LeagueLinksLookup
            .AsNoTracking()
            .Where(l => l.IsActive)
            .Select(l => l.Value);

        var extraction = await playersGeneralInfoExtractor.TryExtarct(leagueLinks);
        
        if (extraction == null)
            return;

        await jsonFileDataManager.SaveData(extraction, "players_general_info_data");
        var extractedLeagues = mapper.Map<IEnumerable<League>>(extraction.Leagues);

        await Console.Out.WriteLineAsync();
    }
}
