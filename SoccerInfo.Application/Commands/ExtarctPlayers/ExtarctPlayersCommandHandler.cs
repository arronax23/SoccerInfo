using AutoMapper;
using SoccerInfo.Extractor;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using SoccerInfoWeb.Server.Data;

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
