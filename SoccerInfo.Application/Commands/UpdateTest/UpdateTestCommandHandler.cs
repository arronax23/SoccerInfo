using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.Extractor;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.Data.Models.Abstractions;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;

namespace SoccerInfo.Application.Commands.UpdateTest;

internal class UpdateTestCommandHandler(
    ApplicationDbContext dbContext,
    SoccerDataExtractor soccerDataExtractor,
    IMapper mapper) : ICommandHandler<UpdateTestCommand>
{
    public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
    {
        string fileName = "scraped_data_3.json";
        string jsonString = File.ReadAllText(fileName);

        ExtractionDto extraction = JsonSerializer.Deserialize<ExtractionDto>(jsonString)!;

        var extractedLeagues = mapper.Map<IEnumerable<League>>(extraction.Leagues);
        EliminateNationalityImageDuplicates(extractedLeagues);


        dbContext.AttachRange(extractedLeagues);

        var dbLeagues = 
            dbContext.Leagues
            .AsNoTracking()
            .Include(x => x.Teams)
            .ThenInclude(y => y.Players)
            .ThenInclude(z => z.NationalityImages);


        foreach (var dbLeague in dbLeagues)
        {
            if (dbLeague != null)
            {
                var extractedLeague = extractedLeagues.SingleOrDefault(dbLeague.Equals);
                ChangeEntityTracking(extractedLeague, dbLeague);

                foreach (var dbTeam in dbLeague.Teams)
                {
                    var extractedTeam = extractedLeague.Teams.SingleOrDefault(dbTeam.Equals);
                    if (extractedTeam != null)
                    {
                        ChangeEntityTracking(extractedTeam, dbTeam);

                        foreach (var dbPlayer in dbTeam.Players)
                        {
                            var extractedPlayer = extractedTeam.Players.SingleOrDefault(dbPlayer.Equals);

                            if (extractedPlayer != null)
                            {
                                ChangeEntityTracking(extractedPlayer, dbPlayer);

                                foreach (var dbNationalityImage in dbPlayer.NationalityImages!)
                                {
                                    var extractedNationalityImage = extractedPlayer!.NationalityImages!.SingleOrDefault(dbNationalityImage.Equals);

                                    if (extractedNationalityImage != null)
                                    {
                                        ChangeEntityTracking(extractedNationalityImage, dbNationalityImage);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        await Console.Out.WriteLineAsync();
   
        await dbContext.SaveChangesAsync();
    }

    public void ChangeEntityTracking<T>(T extractedData, T currentEntity)
        where T : IEntity
    {
        extractedData.Id = currentEntity.Id;
        //dbContext.Entry(currentEntity).State = EntityState.Detached;
        dbContext.Entry(extractedData).State = EntityState.Modified;
    }


    public void EliminateNationalityImageDuplicates(IEnumerable<League>? extractedLeagues)
    {
        var p = extractedLeagues!.SelectMany(x => x.Teams).SelectMany(y => y.Players);
        var im = p.SelectMany(x => x.NationalityImages);

        var duplicates = im
            .GroupBy(n => n.Base64Image)
            .Where(g => g.Count() > 1);

        foreach (var group in duplicates)
        {
            var masterImage = group.First();
            var duplicateImages = group.Skip(1).ToList();

            foreach (var duplicate in duplicateImages)
            {
                var affectedPlayers = p
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
