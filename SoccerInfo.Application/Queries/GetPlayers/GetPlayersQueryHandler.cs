using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctPlayers
{
    internal class GetPlayersQueryHandler : IQueryHandler<GetPlayersQuery, PlayerDto>
    {
        public Task Handle(ExtarctPlayersCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
