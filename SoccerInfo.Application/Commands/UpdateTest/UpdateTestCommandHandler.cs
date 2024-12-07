using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.Extractor;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;

namespace SoccerInfo.Application.Commands.UpdateTest
{
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
            PrepareTeams(extractedLeagues);


            dbContext.AttachRange(extractedLeagues);

            var dbTeams = dbContext.Teams.Include(x => x.Players).ThenInclude(y => y.NationalityImages);

            foreach (var dbTeam in dbTeams)
            {
                var extractedTeam = extractedLeagues.SelectMany(x => x!.Teams!).SingleOrDefault(dbTeam.Equals);
                if (extractedTeam != null)
                {
                    extractedTeam.Id = dbTeam.Id;
                    dbContext.Entry(dbTeam).State = EntityState.Detached;
                    dbContext.Entry(extractedTeam).State = EntityState.Modified;

                    foreach (var dbPlayer in dbTeam.Players)
                    {
                        var extractedPlayer = extractedTeam.Players.SingleOrDefault(dbPlayer.Equals);

                        if (extractedPlayer != null)
                        {
                            extractedPlayer.Id = dbPlayer.Id;
                            dbContext.Entry(dbPlayer).State = EntityState.Detached;
                            dbContext.Entry(extractedPlayer).State = EntityState.Modified;

                            foreach (var dbNationalityImage in dbPlayer.NationalityImages!)
                            {
                                var extractedNationalityImage = extractedPlayer!.NationalityImages!.SingleOrDefault(dbNationalityImage.Equals);

                                if (extractedNationalityImage != null)
                                {
                                    extractedNationalityImage.Id = dbNationalityImage.Id; 
                                    dbContext.Entry(dbNationalityImage).State = EntityState.Detached;
                                    dbContext.Entry(extractedNationalityImage).State = EntityState.Modified;
                                }
                            }
                        }
                    }
                }
            }


            await Console.Out.WriteLineAsync();
       
            await dbContext.SaveChangesAsync();

            //foreach (var dbLeague in dbLeagues)
            //{
            //    var extractedLeague = extractedLeagues.SingleOrDefault(dbLeague.Equals);
            //    if (extractedLeague != null)
            //    {
            //        extractedLeague.Id = dbLeague.Id;
            //        foreach (var dbTeam in dbLeague.Teams)
            //        {
            //            var extractedTeam = extractedLeague.Teams.SingleOrDefault(dbTeam.Equals);
            //            if (extractedTeam != null)
            //            {
            //                extractedTeam.Id = dbTeam.Id;
            //                foreach (var dbPlayer in dbTeam.Players)
            //                {
            //                    var extractedPlayer = extractedTeam.Players.SingleOrDefault(dbPlayer.Equals);
            //                    if (extractedPlayer != null)
            //                    {
            //                        extractedPlayer.Id = dbPlayer.Id;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

        }

        private void PrepareTeams(IEnumerable<League> leagues)
        {
            foreach (var league in leagues)
            {
                foreach (var team in league.Teams!)
                {
                    team.League = league;
                }
            }
        }
    }
}
