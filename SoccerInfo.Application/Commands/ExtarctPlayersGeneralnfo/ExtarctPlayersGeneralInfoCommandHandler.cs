using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.JsonFileData;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;

internal class ExtarctPlayersGeneralInfoCommandHandler(
    ApplicationDbContext dbContext,
    PlayersGeneralInfoExtractor playersGeneralInfoExtractor,
    JsonFileDataManager jsonFileDataManager) : ICommandHandler<ExtarctPlayersGeneralnfoCommand, GeneralInfoExtractionData?>
{
    public async Task<GeneralInfoExtractionData?> Handle(ExtarctPlayersGeneralnfoCommand request, CancellationToken cancellationToken)
    {
        var leagueLinks = dbContext.LeagueLinksLookup
            .AsNoTracking()
            .Where(l => l.IsActive)
            .Select(l => l.Value);

        var extraction = await playersGeneralInfoExtractor.TryExtarct(leagueLinks);

        if (extraction == null)
            await jsonFileDataManager.SaveData(extraction, "players_general_info_data");

        return extraction;
    }
}
