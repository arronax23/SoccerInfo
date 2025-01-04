using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.Data.Models.Abstractions;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.Application.Commands.UpdateToEnglish;

internal class UpdateToEnglishCommandHandler(
    ApplicationDbContext dbContext,
    UpdateNationalitiesToEnglishService updateNationalitiesToEnglishService,
    IMapper mapper) : ICommandHandler<UpdateToEnglishCommand>
{
    public async Task Handle(UpdateToEnglishCommand request, CancellationToken cancellationToken)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        string fileName = "scraped_data_8.json";
        string jsonString = File.ReadAllText(fileName);

        GeneralInfoExtractionData extraction = JsonSerializer.Deserialize<GeneralInfoExtractionData>(jsonString)!;
        var extractedLeagues = mapper.Map<IEnumerable<League>>(extraction.Leagues);

        EliminateNationalityDuplicates(extractedLeagues);
        dbContext.AttachRange(extractedLeagues);

        var extractedNationalities = extractedLeagues
            .SelectMany(x => x.Teams!)
            .SelectMany(y => y.Players!)
            .SelectMany(z => z.Nationalities!)
            .GroupBy(x => x.GetHashCode())
            .Select(x => x.First())
            .ToList();

        await updateNationalitiesToEnglishService.Update(extractedNationalities);

        //ChangeNationalitiesTracking(extractedLeagues);

        var dbLeagues =
            dbContext.Leagues
            .AsNoTracking()!
            .AsSplitQuery()!
            .Include(x => x.Teams)!
            .ThenInclude(y => y.Players)!
            .ThenInclude(z => z.Nationalities);


        foreach (var dbLeague in dbLeagues)
        {
            var extractedLeague = extractedLeagues.SingleOrDefault(dbLeague.Equals);
            if (extractedLeague != null)
            {
                ChangeEntityTracking<League, LeagueData>(extractedLeague, dbLeague);

                foreach (var dbTeam in dbLeague.Teams!)
                {
                    var extractedTeam = extractedLeague.Teams!.SingleOrDefault(dbTeam.Equals);
                    if (extractedTeam != null)
                    {
                        ChangeEntityTracking<Team, TeamData>(extractedTeam, dbTeam);

                        foreach (var dbPlayer in dbTeam.Players!)
                        {
                            var extractedPlayer = extractedTeam.Players!.SingleOrDefault(dbPlayer.Equals);
                            if (extractedPlayer != null)
                            {
                                ChangeEntityTracking<Player, PlayerData>(extractedPlayer, dbPlayer);
                            }
                        }
                    }
                }
            }
        }

        ClearNavgationData(extractedLeagues);

        var entries = dbContext.ChangeTracker.Entries().ToList();
        var added = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
        var modified = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
        var unchanged = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged).ToList();
        var detached = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Detached).ToList();

        var bds = extractedLeagues.SelectMany(x => x.Teams!).SelectMany(y => y.Players!).SelectMany(z => z.Nationalities!).Count();

        await dbContext.SaveChangesAsync();

        await transaction.RollbackAsync();
    }


    private void ChangeEntityTracking<T, TData>(T extractedEntity, T currentEntity)
        where T : IEntity
    {
        extractedEntity.Id = currentEntity.Id;
        dbContext.Entry(extractedEntity).State = EntityState.Unchanged;
        var extractedEntityProperties = dbContext.Entry(extractedEntity).Properties;

        foreach (var extractedProperty in extractedEntityProperties)
        {
            if (typeof(TData).GetProperties().Select(x => x.Name).Contains(extractedProperty.Metadata.Name))
                extractedProperty.IsModified = true;
        }
    }

    private void ChangeNationalitiesTracking(IEnumerable<League>? extractedLeagues)
    {
        var nationalities = extractedLeagues!
            .SelectMany(x => x.Teams!).
            SelectMany(y => y.Players!).
            SelectMany(z => z.Nationalities!);

        foreach (var dbImage in dbContext.Nationalities.AsNoTracking())
        {
            var newImage = nationalities
                .Where(x => x.Country == dbImage.Country_Lookup)
                .FirstOrDefault();

            if (newImage != null)
                ChangeEntityTracking<Nationality, NationalityData>(newImage, dbImage);
        }
    }


    private void ClearNavgationData(IEnumerable<League> extractedLeagues)
    {
        var np = extractedLeagues
            .SelectMany(x => x.Teams!)
            .SelectMany(y => y.Players!)
            .SelectMany(z => z.Nationalities!);

        foreach (var n in np)
        {
            foreach (var p in n.Players!.Where(p => p.Id != 0))
            {
                n.Players.Remove(p);
            }
        }
        //foreach (var nationality in nationalities)
        //{
        //    nationality.Players = null;
        //}
    }

    private class NationalityPlayer
    {
        public Nationality Nationality { get; set; } = null!;
        public IEnumerable<Player> Players { get; set; } = null!;
    }

    private void EliminateNationalityDuplicates(IEnumerable<League>? extractedLeagues)
    {
        var players = extractedLeagues!.SelectMany(x => x.Teams!).SelectMany(y => y.Players!);
        var nationalities = players.SelectMany(x => x.Nationalities!);

        var duplicates = nationalities
            .GroupBy(n => n.Country)
            .Where(g => g.Count() > 1);

        foreach (var group in duplicates)
        {
            var masterNationality = group.First();
            var duplicateImages = group.Skip(1);

            foreach (var duplicate in duplicateImages)
            {
                var affectedPlayers = players
                    .Where(p => p.Nationalities != null && p.Nationalities.Contains(duplicate));

                foreach (var player in affectedPlayers)
                {
                    player!.Nationalities!.Remove(duplicate);

                    if (!player.Nationalities.Contains(masterNationality))
                    {
                        player.Nationalities.Add(masterNationality);
                    }
                }
            }
        }
    }
}

