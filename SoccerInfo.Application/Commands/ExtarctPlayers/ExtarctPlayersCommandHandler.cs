using AutoMapper;
using SoccerInfo.Extractor;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using SoccerInfoWeb.Server.Data;

namespace SoccerInfo.Application.Commands.ExtarctPlayers
{
    internal class ExtarctPlayersCommandHandler(
        ApplicationDbContext dbContext,
        TransfermarktExtractor transfermarktExtractor,
        IMapper mapper) : ICommandHandler<ExtarctPlayersCommand>
    {
        public async Task Handle(ExtarctPlayersCommand request, CancellationToken cancellationToken)
        {
            var extraction = await transfermarktExtractor.Extarct();

            var teams = extraction.Extraction.Select(x => new Team()
            {
                Name = x.TeamName,
                Players = mapper.Map<ICollection<Player>>(x.Players)
            });


            await dbContext.Teams.AddRangeAsync(teams);
            await dbContext.SaveChangesAsync();
        }
    }
}
