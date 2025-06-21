using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.JsonFileData;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Extraction;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;

internal class ExtarctPlayersGeneralInfoCommandHandler(
    IGenericRepository<LeagueLinkLookup> leagueLinkLookupRepository,
    PlayersGeneralInfoExtractor playersGeneralInfoExtractor,
    IJsonFileDataManager jsonFileDataManager) : ICommandHandler<ExtarctPlayersGeneralnfoCommand, GeneralInfoExtractionData?>
{
    public async Task<GeneralInfoExtractionData?> Handle(ExtarctPlayersGeneralnfoCommand request, CancellationToken cancellationToken)
    {

        var leagueLinks = leagueLinkLookupRepository
            .ToQuery()
            .AsNoTracking()
            .Where(l => l.IsActive)
            .Select(l => l.Value);

        var extraction = await playersGeneralInfoExtractor.TryExtarct(leagueLinks);

        if (extraction == null)
            await jsonFileDataManager.SaveData(extraction, "players_general_info_data");

        return extraction;
    }
}
