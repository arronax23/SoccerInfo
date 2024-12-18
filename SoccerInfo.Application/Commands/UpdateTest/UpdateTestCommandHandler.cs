using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.Data.Models.Abstractions;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;
using static SoccerInfo.Extractor.Dto.ExtractionData;

namespace SoccerInfo.Application.Commands.UpdateTest;

internal class UpdateTestCommandHandler(
    ApplicationDbContext dbContext,
    IMapper mapper) : ICommandHandler<UpdateTestCommand>
{
    public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        string fileName = "scraped_data_6.json";
        string jsonString = File.ReadAllText(fileName);

        ExtractionData extraction = JsonSerializer.Deserialize<ExtractionData>(jsonString)!;
        var extractedLeagues = mapper.Map<IEnumerable<League>>(extraction.Leagues);

        EliminateNationalityImageDuplicates(extractedLeagues);
        dbContext.AttachRange(extractedLeagues);


        ChangeNationalityImagesTracking(extractedLeagues);

        var dbLeagues = 
            dbContext.Leagues
            .AsNoTracking()!
            .AsSplitQuery()!
            .Include(x => x.Teams)!
            .ThenInclude(y => y.Players)!
            .ThenInclude(z => z.NationalityImages);


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

        var e = dbContext.ChangeTracker.Entries().ToList();
        var e2 = dbContext.ChangeTracker.Entries().Where(x=> x.State == EntityState.Added).ToList();
        var e3 = dbContext.ChangeTracker.Entries().Where(x=> x.State == EntityState.Modified).ToList();
        var e4 = dbContext.ChangeTracker.Entries().Where(x=> x.State == EntityState.Unchanged).ToList();
        var e5 = dbContext.ChangeTracker.Entries().Where(x=> x.State == EntityState.Detached).ToList();

        var bds = extractedLeagues.SelectMany(x => x.Teams!).SelectMany(y => y.Players!).SelectMany(z => z.NationalityImages!).Count();

        await dbContext.SaveChangesAsync();    

        await transaction.CommitAsync();
    }


    private void ChangeEntityTracking<T,TData>(T extractedEntity, T currentEntity)
        where T : IEntity
    {
        extractedEntity.Id = currentEntity.Id;
        dbContext.Entry(extractedEntity).State = EntityState.Unchanged;
        var extractedEntityProperties = dbContext.Entry(extractedEntity).Properties;

        foreach (var extractedProperty in extractedEntityProperties)
        {
            if (typeof(TData).GetProperties().Select(x=> x.Name).Contains(extractedProperty.Metadata.Name))
                extractedProperty.IsModified = true;
        }
    }

    private void ChangeNationalityImagesTracking(IEnumerable<League>? extractedLeagues)
    {
        var nationalityImages = extractedLeagues!
            .SelectMany(x => x.Teams!).
            SelectMany(y => y.Players!).
            SelectMany(z => z.NationalityImages!);

        foreach (var dbImage in dbContext.NationalityImages.AsNoTracking())
        {
            var newImage = nationalityImages
                .Where(x => x.Equals(dbImage))
                .FirstOrDefault();

            if (newImage != null)
                ChangeEntityTracking<NationalityImage, NationalityImageData>(newImage, dbImage);
        }
    }


    private void ClearNavgationData(IEnumerable<League> extractedLeagues)
    {
        var nationalityImages = extractedLeagues
            .SelectMany(x => x.Teams!)
            .SelectMany(y => y.Players!)
            .SelectMany(z => z.NationalityImages!);

        foreach (var image in nationalityImages)
        {
            image.Players = null;
        }
    }

    private void EliminateNationalityImageDuplicates(IEnumerable<League>? extractedLeagues)
    {
        var players = extractedLeagues!.SelectMany(x => x.Teams!).SelectMany(y => y.Players!);
        var images = players.SelectMany(x => x.NationalityImages!);

        var duplicates = images
            .GroupBy(n => n.Base64Image)
            .Where(g => g.Count() > 1);

        foreach (var group in duplicates)
        {
            var masterImage = group.First();
            var duplicateImages = group.Skip(1);

            foreach (var duplicate in duplicateImages)
            {
                var affectedPlayers = players
                    .Where(p => p.NationalityImages != null && p.NationalityImages.Contains(duplicate));

                foreach (var player in affectedPlayers)
                {
                    player!.NationalityImages!.Remove(duplicate);

                    if (!player.NationalityImages.Contains(masterImage))
                    {
                        player.NationalityImages.Add(masterImage);
                    }
                }
            }
        }
    }
}
