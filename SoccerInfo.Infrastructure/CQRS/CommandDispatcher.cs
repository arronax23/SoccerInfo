using MediatR;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Infrastructure.CQRS;

public class CommandDispatcher(IMediator mediator) : ICommandDispatcher
{
    public async Task Send(ICommand command)
    {
        await mediator.Send(command);
    }

    public async Task Send(ICommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }

}
