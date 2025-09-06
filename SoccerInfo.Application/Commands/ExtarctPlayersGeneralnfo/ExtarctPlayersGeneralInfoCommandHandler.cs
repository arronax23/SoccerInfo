using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Services;
using SoccerInfo.Domain.Models.Extraction;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Domain.Models.Extraction.ExtractionInfo;

namespace SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;

internal class ExtarctPlayersGeneralInfoCommandHandler(
    IGenericRepository<LeagueLinkLookup> leagueLinkLookupRepository,
    ExtractionInfoService extractionInfoService,
    PlayersGeneralInfoExtractor playersGeneralInfoExtractor)
    : ICommandHandler<ExtarctPlayersGeneralnfoCommand, GeneralInfoExtractionData?>
{
    public async Task<GeneralInfoExtractionData?> Handle(ExtarctPlayersGeneralnfoCommand request, CancellationToken cancellationToken)
    {
        var leagueLinks = leagueLinkLookupRepository
            .ToQuery()
            .AsNoTracking()
            .Where(l => l.IsActive)
            .Select(l => l.Value);


        var extractionData = await extractionInfoService.Use(async () =>
        {
            return await playersGeneralInfoExtractor.TryExtarct(leagueLinks);
        },
        ExtractionType.PlayersGeneralInfo, saveToFile: true);


        return extractionData;
    }
}
