using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Persistence.Transactions;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristics;

internal class SavePlayersCharacteristicsCommandHandler(
    IConfiguration configuration,
    ApplicationDbContext dbContext,
    IMapper mapper) 
    : ICommandHandler<SavePlayersCharacteristicsCommand>
{

    private readonly HashSet<StatsLeague> _uniqueLeagues = new();
    public async Task Handle(SavePlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        string fileName = request.FileName.Trim();
        string jsonString = File.ReadAllText(Path.Combine("JsonData", fileName));

        PlayersCharacteristicsExtractionData extraction = 
            JsonSerializer.Deserialize<PlayersCharacteristicsExtractionData>(jsonString)!;

        var dbPlayers = dbContext.Players;

        var dbStatsLeagues = dbContext.StatsLeagues.ToList();

        foreach (var extractedCharacteristic in extraction!.PlayersCharacteristics)
        {
            var dbPlayer = dbPlayers.Single(x => x.TransfermarktId == extractedCharacteristic.TransfermarktId);

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

        var entries = dbContext.ChangeTracker.Entries().ToList();

        var added = entries.Where(x => x.State == EntityState.Added).ToList();
        var modified = entries.Where(x => x.State == EntityState.Modified).ToList();
        var unchanged = entries.Where(x => x.State == EntityState.Unchanged).ToList();

        dbContext.SaveChanges();
        await transaction.ResolveAsync(configuration);    

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
