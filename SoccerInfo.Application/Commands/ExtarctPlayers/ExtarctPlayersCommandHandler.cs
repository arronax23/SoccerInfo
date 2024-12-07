using AutoMapper;
using SoccerInfo.Extractor;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.Application.Commands.ExtarctPlayers
{
    internal class ExtarctPlayersCommandHandler(
        ApplicationDbContext dbContext,
        SoccerDataExtractor soccerDataExtractor,
        IMapper mapper) : ICommandHandler<ExtarctPlayersCommand>
    {
        public async Task Handle(ExtarctPlayersCommand request, CancellationToken cancellationToken)
        {
            var extraction = await soccerDataExtractor.Extarct();


            await JsonSerializerToFile.Save(extraction, "scraped_data_3.json");
            var extractedLeagues = mapper.Map<IEnumerable<League>>(extraction.Leagues);

            await Console.Out.WriteLineAsync();

            var dbLeagues = dbContext.Leagues;



            foreach (var dbLeague in dbLeagues) 
            {
                var extractedLeague = extractedLeagues.SingleOrDefault(dbLeague.Equals);
                if (extractedLeague != null)
                {
                    extractedLeague.Id = dbLeague.Id;
                    foreach (var dbTeam in dbLeague.Teams)
                    {
                        var extractedTeam = extractedLeague.Teams.SingleOrDefault(dbTeam.Equals);
                        if (extractedTeam != null)
                        {
                            extractedTeam.Id = dbTeam.Id;
                            foreach (var dbPlayer in dbTeam.Players)
                            {
                                var extractedPlayer = extractedTeam.Players.SingleOrDefault(dbPlayer.Equals);
                                if (extractedPlayer != null)
                                {
                                    extractedPlayer.Id = dbPlayer.Id;
                                }
                            }
                        }
                    }
                }
            }

    
            //var teams = extraction.Leagues.Select(x => new Team()
            //{
            //    Name = x.TeamName,
            //    Players = mapper.Map<ICollection<Player>>(x.Players)
            //});


            //await dbContext.Teams.AddRangeAsync(teams);
            //await dbContext.SaveChangesAsync();
        }
    }
}
