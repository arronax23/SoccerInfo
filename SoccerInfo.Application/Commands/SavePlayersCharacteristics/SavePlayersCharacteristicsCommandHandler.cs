using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristics;

internal class SavePlayersCharacteristicsCommandHandler(
    ApplicationDbContext dbContext,
    IMapper mapper) 
    : ICommandHandler<SavePlayersCharacteristicsCommand>
{

    private readonly HashSet<StatsLeague> _uniqueLeagues = new();
    public async Task Handle(SavePlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        string fileName = request.FileName.Trim();
        string jsonString = File.ReadAllText(fileName);

        PlayersCharacteristicsExtractionData extraction = 
            JsonSerializer.Deserialize<PlayersCharacteristicsExtractionData>(jsonString)!;

        var dbPlayers = dbContext.Players
            .Include(x=> x.Characteristics)
            .ThenInclude(y => y.Socials)
            .Include(x => x.Characteristics)!
            .ThenInclude(y => y.OutfieldPlayerStats)!
            .ThenInclude(z => z.League)!
            .Include(x => x.Characteristics)!
            .ThenInclude(y => y.GoalKeeperStats)!
            .ThenInclude(z => z.League);

        var dbStatsLeagues = dbContext.StatsLeagues.ToList();

        foreach (var extractedCharacteristic in extraction!.PlayersCharacteristics)
        {
            var dbPlayer = dbPlayers.Single(x => x.TransfermarktId == extractedCharacteristic.TransfermarktId);

            dbPlayer.UpdateGeneralCharacteristics(
                extractedCharacteristic.Height,
                extractedCharacteristic.LeadingFoot!,
                extractedCharacteristic.ClubJoinDate,
                extractedCharacteristic.ContractExpirationDate);

            dbPlayer.UpdateBirthPlace(
                extractedCharacteristic.BrithPlace!.City!,
                extractedCharacteristic.BrithPlace.Country!);

            if (extractedCharacteristic.NationalTeam != null)
            {
                dbPlayer.UpdateNationalTeam(
                    extractedCharacteristic.NationalTeam.Country!,
                    extractedCharacteristic.NationalTeam.Caps,
                    extractedCharacteristic.NationalTeam.Goals);
            } 

            dbPlayer.UpdateSocials(mapper.Map<IEnumerable<SocialMedia>>(extractedCharacteristic.Socials));

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
        await transaction.CommitAsync();    

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
