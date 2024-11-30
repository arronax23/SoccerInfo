using MediatR;

namespace SoccerInfo.Shared.CQRS;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}
