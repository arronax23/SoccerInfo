using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Persistence.EntityFrameworkExtensions;
using SoccerInfo.Shared.CQRS;
using System.Text.RegularExpressions;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristics;

internal class SavePlayersCharacteristicsFromFileCommandHandler(
    ILogger<SavePlayersCharacteristicsFromFileCommandHandler> logger,
    IConfiguration configuration,
    ApplicationDbContext dbContext,
    IMapper mapper) 
    : ICommandHandler<SavePlayersCharacteristicsCommand>
{

    private readonly HashSet<StatsLeague> _uniqueLeagues = new();
    public async Task Handle(SavePlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Executing SavePlayersCharacteristicsCommand...");

        using var transaction = dbContext.Database.BeginTransaction();

        var dbPlayers = dbContext.Players;
        var dbStatsLeagues = dbContext.StatsLeagues.ToList();

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

            if (extractedCharacteristic.IsGoalkeeper)
            {
                foreach (var goalKeeperStatsItem in extractedCharacteristic.GoalKeeperStats!)
                {
                    var statsItem = mapper.Map<GoalKeeperStats>(goalKeeperStatsItem);
                    EliminateStatsLeaguesDuplicates(dbStatsLeagues, statsItem);

                    dbPlayer.UpdateStats(statsItem);
                }
            }
            else
            {
                foreach (var outfieldPlayerStatsItem in extractedCharacteristic.OutfieldPlayerStats!)
                {
                    var statsItem = mapper.Map<OutfieldPlayerStats>(outfieldPlayerStatsItem);
                    EliminateStatsLeaguesDuplicates(dbStatsLeagues, statsItem);

                    dbPlayer.UpdateStats(statsItem);
                }
            }

        }
        dbContext.ChangeTracker.ShowEntries();

        await dbContext.SaveChangesAsync();
        await transaction.ResolveAsync(configuration);

        logger.LogInformation("Executing SavePlayersCharacteristicsCommand successfully saved data");

    }


    private void EliminateStatsLeaguesDuplicates(List<StatsLeague> dbStatsLeagues, StatsBase statsItem)
    {
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
