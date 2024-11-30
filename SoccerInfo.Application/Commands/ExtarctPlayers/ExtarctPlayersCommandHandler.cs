using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctPlayers
{
    internal class ExtarctPlayersCommandHandler : ICommandHandler<ExtarctPlayersCommand>
    {
        public Task Handle(ExtarctPlayersCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
