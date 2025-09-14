using AutoMapper;
using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Domain.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;
using System.Text.RegularExpressions;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristics;

internal class SavePlayersCharacteristicsFromFileCommandHandler(
    ILogger<SavePlayersCharacteristicsFromFileCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository,
    IGenericRepository<StatsLeague> statsLeagueRepository,
    IMapper mapper) 
    : ICommandHandler<SavePlayersCharacteristicsCommand>
{

    private readonly HashSet<StatsLeague> _uniqueLeagues = new();
    public async Task Handle(SavePlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Executing SavePlayersCharacteristicsCommand...");

        using var transaction = unitOfWork.BeginTransaction();

        var dbPlayers = playerRepository.ToQuery();
        var dbStatsLeagues = statsLeagueRepository.ToQuery().ToList();

        foreach (var extractedCharacteristic in request.Extraction!.PlayersCharacteristics)
        {
            var dbPlayer = dbPlayers.SingleOrDefault(x => x.TransfermarktId == extractedCharacteristic.TransfermarktId);
            
            if (dbPlayer is null)
            {
                logger.LogWarning($"Player with TransfermarktId: {extractedCharacteristic.TransfermarktId} not found in db");
                continue;
            }

            dbPlayer.UpdateGeneralCharacteristics(
                extractedCharacteristic.Height,
                extractedCharacteristic.LeadingFoot!,
                extractedCharacteristic.ClubJoinDate,
                extractedCharacteristic.ContractExpirationDate);


            if (extractedCharacteristic.BrithPlace != null)
            {
                dbPlayer.UpdateBirthPlace(
                    extractedCharacteristic.BrithPlace.City!,
                    extractedCharacteristic.BrithPlace.Country!);
            }

            if (extractedCharacteristic.NationalTeam != null)
            {
                dbPlayer.UpdateNationalTeam(
                    extractedCharacteristic.NationalTeam.Name!,
                    extractedCharacteristic.NationalTeam.Country!,
                    extractedCharacteristic.NationalTeam.Caps,
                    extractedCharacteristic.NationalTeam.Goals);
            }

            if (extractedCharacteristic.Socials != null)
            { 
                dbPlayer.UpdateSocials(mapper.Map<IEnumerable<SocialMedia>>(extractedCharacteristic.Socials));
            }

            dbPlayer.ClearStats();

            if (extractedCharacteristic.IsGoalkeeper)
            {
                foreach (var goalKeeperStatsItem in extractedCharacteristic.GoalKeeperStats!)
                {
                    var statsItem = mapper.Map<GoalKeeperStats>(goalKeeperStatsItem);
                    EliminateStatsLeaguesDuplicates(dbStatsLeagues, statsItem, extractedCharacteristic);

                    dbPlayer.UpdateStats(statsItem);
                }
            }
            else
            {
                foreach (var outfieldPlayerStatsItem in extractedCharacteristic.OutfieldPlayerStats!)
                {
                    var statsItem = mapper.Map<OutfieldPlayerStats>(outfieldPlayerStatsItem);
                    EliminateStatsLeaguesDuplicates(dbStatsLeagues, statsItem, extractedCharacteristic);

                    dbPlayer.UpdateStats(statsItem);
                }
            }

        }
        unitOfWork.ShowEntires();

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.ResolveTransactionAsync(transaction);

        logger.LogInformation("Executing SavePlayersCharacteristicsCommand successfully saved data");

    }

    private void EliminateStatsLeaguesDuplicates(List<StatsLeague> dbStatsLeagues, StatsBase statsItem, PlayerCharacteristicsData playerCharacteristicsData)
    {
        if (statsItem.League.Base64Image == null) 
        {
            logger.LogCritical("Error statsItem.League.Base64Image is null");
            logger.LogCritical($"Name: {statsItem.League.Name}");    
            logger.LogCritical($"Base64Image: {statsItem.League.Base64Image}");    
            logger.LogCritical($"TransfermarktId: {playerCharacteristicsData.TransfermarktId}");    
        }

        statsItem.League.Base64Image = Regex.Unescape(statsItem.League.Base64Image!).Trim();
        var dbStatsLeague = dbStatsLeagues.SingleOrDefault(x => x.Equals(statsItem.League));

        if (dbStatsLeague != null)
        {
            statsItem.UpdateLeague(dbStatsLeague);
        }
        else
        {
            _uniqueLeagues.Add(statsItem.League);
            statsItem.UpdateLeague(_uniqueLeagues.Single(x => x.Equals(statsItem.League)));
        }
    }
}
