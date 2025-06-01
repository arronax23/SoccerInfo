//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using SoccerInfo.Persistence.Data;
//using SoccerInfo.Persistence.Data.Models;
//using SoccerInfo.Persistence.Data.Models.Abstractions;
//using SoccerInfo.Persistence.Data.Models.GeneralPosition;
//using SoccerInfo.Persistence.EntityFrameworkExtensions;
//using SoccerInfo.Shared.CQRS;
//using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

//namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfo;

//internal class SavePlayersGeneralInfoCommandHandler2(
//    IConfiguration configuration,
//    ApplicationDbContext dbContext,
//    IMapper mapper,
//    GeneralPositionService generalPositionService) : ICommandHandler<SavePlayersGeneralInfoCommand>
//{
//    public async Task Handle(SavePlayersGeneralInfoCommand request, CancellationToken cancellationToken)
//    {
//        using var transaction = dbContext.Database.BeginTransaction();

//        var extractedLeagues = mapper.Map<IEnumerable<League>>(request.Extraction.Leagues);

//        EliminateNationalityDuplicates(extractedLeagues);
//        dbContext.AttachRange(extractedLeagues);

//        AttachGeneralPosition(extractedLeagues);

//        ChangeNationalitiesTracking(extractedLeagues);

//        var dbCountryFlags = dbContext.CountryFlags_Lookup.AsNoTracking();

//        var dbLeagues = dbContext.Leagues.AsNoTracking();

//        foreach (var dbLeague in dbLeagues)
//        { 
//            var extractedLeague = extractedLeagues.SingleOrDefault(dbLeague.Equals);
//            if (extractedLeague != null)
//            {
//                ChangeEntityTracking<League, LeagueData>(extractedLeague, dbLeague);

//                foreach (var dbTeam in dbLeague.Teams!)
//                {
//                    var extractedTeam = extractedLeague.Teams!.SingleOrDefault(dbTeam.Equals);
//                    if (extractedTeam != null)
//                    {
//                        ChangeEntityTracking<Team, TeamData>(extractedTeam, dbTeam);

//                        foreach (var dbPlayer in dbTeam.Players!)
//                        {
//                            var extractedPlayer = extractedTeam.Players!.SingleOrDefault(dbPlayer.Equals);
//                            if (extractedPlayer != null)
//                            {
//                                ChangeEntityTracking<Player, PlayerData>(extractedPlayer, dbPlayer);
//                            }
//                        }
//                    }
//                }
//            } 
//        }

//        var entries = dbContext.ChangeTracker.Entries().ToList();
//        var added = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
//        var modified = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
//        var unchanged = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged).ToList();
//        var detached = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Detached).ToList();

//        var bds = extractedLeagues.SelectMany(x => x.Teams!).SelectMany(y => y.Players!).SelectMany(z => z.Nationalities!).Count();

//        await dbContext.SaveChangesAsync();    

//        await transaction.ResolveAsync(configuration);
//    }


//    private void ChangeEntityTracking<T,TData>(T extractedEntity, T currentEntity)
//        where T : IEntity
//    {
//        extractedEntity.Id = currentEntity.Id;
//        dbContext.Entry(extractedEntity).State = EntityState.Unchanged;
//        var extractedEntityProperties = dbContext.Entry(extractedEntity).Properties;

//        foreach (var extractedProperty in extractedEntityProperties)
//        {
//            if (typeof(TData).GetProperties().Select(x=> x.Name).Contains(extractedProperty.Metadata.Name))
//                extractedProperty.IsModified = true;
//        }
//    }

//    private void ChangeNationalitiesTracking(IEnumerable<League>? extractedLeagues)
//    {
//        var nationalities = extractedLeagues!
//            .SelectMany(x => x.Teams!)
//            .SelectMany(y => y.Players!)
//            .SelectMany(z => z.Nationalities!);

//        foreach (var extractedNationality in nationalities)
//        {
//            var dbNationalities = dbContext.Nationalities.AsNoTracking();
//            var dbCountryLookups = dbContext.CountryFlags_Lookup.AsNoTracking();
//            var dbNationality = dbNationalities.FirstOrDefault(x => x.Country == extractedNationality.Country || x.Country_Lookup == extractedNationality.Country);

//            if (dbNationality != null)
//                ChangeEntityTracking<Nationality, NationalityData>(extractedNationality, dbNationality);
//            else
//            {
//                var lookup = dbCountryLookups.FirstOrDefault(x => extractedNationality.Country == x.Name);
//                if (lookup is not null)
//                {
//                    extractedNationality.CountryFlagId = lookup.Id;
//                    extractedNationality.Country_Lookup = lookup.Name;
//                }
//            }
//        }
//    }

//    private void EliminateNationalityDuplicates(IEnumerable<League>? extractedLeagues)
//    {
//        var players = extractedLeagues!.SelectMany(x => x.Teams!).SelectMany(y => y.Players!);
//        var images = players.SelectMany(x => x.Nationalities!);

//        var duplicates = images
//            .GroupBy(n => n.Country)
//            .Where(g => g.Count() > 1);

//        foreach (var group in duplicates)
//        {
//            var masterImage = group.First();
//            var duplicateImages = group.Skip(1);

//            foreach (var duplicate in duplicateImages)
//            {
//                var affectedPlayers = players
//                    .Where(p => p.Nationalities != null && p.Nationalities.Contains(duplicate));

//                foreach (var player in affectedPlayers)
//                {
//                    player!.Nationalities!.Remove(duplicate);

//                    if (!player.Nationalities.Contains(masterImage))
//                    {
//                        player.Nationalities.Add(masterImage);
//                    }
//                }
//            }
//        }
//    }

//    private void AttachGeneralPosition(IEnumerable<League> extractedLeagues)
//    {
//        var players = extractedLeagues.SelectMany(x => x.Teams!).SelectMany(y => y.Players!);
//        generalPositionService.AttachGeneralPosition(players);
//    }
//}
